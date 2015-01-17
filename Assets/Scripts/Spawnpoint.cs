using UnityEngine;
using System.Collections;

public class Spawnpoint : MonoBehaviour
{
	public float waveLength = 10.0f;
	public float timeBetweenWaves = 5.0f;
	public int numWaves = 3;
	public int spawnsPerWave = 5;

	private float mWaveTimeLeft = 0.0f;
	private float mTimeLeftBetweenWaves = 0.0f;
	private int mWavesDone = 0;
	private int mNumSpawned = 0;

	// gameobject to be spawned
	public GameObject enemy = null;
	
	// destination (where all spawned objects run to)
	public Transform destination = null;

	public GameObject inGameMenu = null; // for game state, etc.
	private MenuInGame menuObject = null;

	void Start()
	{
		if (inGameMenu != null)
		{
			menuObject = inGameMenu.GetComponent<MenuInGame>();
		}

		Reset ();
	}

	void Reset()
	{
		mWaveTimeLeft = waveLength / (float)spawnsPerWave;
		mTimeLeftBetweenWaves = timeBetweenWaves;
		mWavesDone = 0;
		mNumSpawned = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		// Check for state changes
		if (menuObject.GetState() != menuObject.GetLastState())
		{
			Debug.Log ("SPAWNPOINT: State changed... Reset.");
			Reset ();
		}

		if (menuObject.GetState() == MenuInGame.INGAME_STATE.INGAME_STATE_DEFEND)
		{
			// Check if the day is done.
			if (mWavesDone >= numWaves)
			{
				GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
				if (enemyList.Length == 0)
				{
					Debug.Log ("SPAWNPOINT: Day complete.");
					menuObject.SetDayComplete();
					mNumSpawned = 0;
				}
			}
			// Count down to the next wave.
			else if (mTimeLeftBetweenWaves > 0.0f)
			{
				mTimeLeftBetweenWaves -= Time.deltaTime;
			}
			// Check if the current wave is done.
			else if (mNumSpawned >= spawnsPerWave)
			{
				mWavesDone++;
				Debug.Log ("SPAWNPOINT: Wave " + mWavesDone + " complete.");

				// Reset data for the next wave.
				mWaveTimeLeft = waveLength / (float)spawnsPerWave;
				mTimeLeftBetweenWaves = timeBetweenWaves;
				mNumSpawned = 0;
			}
			// Count down to the next spawn.
			else
			{
				mWaveTimeLeft -= Time.deltaTime;
				if (mWaveTimeLeft <= 0.0f)
				{
					mNumSpawned++;
					Debug.Log ("SPAWNPOINT: Spawned enemy #" + mNumSpawned);

					// Reset timer for next spawn.
					mWaveTimeLeft = waveLength / (float)spawnsPerWave;

					GameObject newEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
					NavMeshAgent nav = newEnemy.GetComponent<NavMeshAgent>();
					nav.destination = destination.position;
				}
			}
		}
	}
}
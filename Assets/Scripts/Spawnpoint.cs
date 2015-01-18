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
	private MenuInGame mMenuObject = null;

	public GameObject notificationMenu = null;
	private MenuNotifications mMenuNotifications = null;

	private int mDay = 0;

	void Start()
	{
		if (inGameMenu != null)
		{
			mMenuObject = inGameMenu.GetComponent<MenuInGame>();
		}
		if (notificationMenu != null)
		{
			mMenuNotifications = notificationMenu.GetComponent<MenuNotifications>();
		}

		Reset ();
	}

	public void Reset()
	{
		mWaveTimeLeft = waveLength / (float)spawnsPerWave;
		mTimeLeftBetweenWaves = timeBetweenWaves;
		mWavesDone = 0;
		mNumSpawned = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		if (mMenuObject.GetState() == MenuInGame.INGAME_STATE.INGAME_STATE_DEFEND)
		{
			// Check if the day is done.
			if (mWavesDone >= numWaves)
			{
				if (AreAllEnemiesDead())
				{
					mDay++;
					string s = "Day " + mDay + " complete!";
					mMenuNotifications.SetText(s);
					Debug.Log ("SPAWNPOINT: Day complete.");

					mMenuObject.SetDayComplete();

					// Reset for the next day.
					Reset ();
				}
			}
			// Count down to the next wave.
			else if (mTimeLeftBetweenWaves > 0.0f)
			{
				mTimeLeftBetweenWaves -= Time.deltaTime;

				string s = "Next Wave: " + (int)mTimeLeftBetweenWaves + "s";
				mMenuNotifications.SetText(s);
			}
			// Check if the current wave is done.
			else if (mNumSpawned >= spawnsPerWave)
			{
				if (AreAllEnemiesDead())
				{
					mWavesDone++;

					string s = "Wave " + mWavesDone + " complete!";
					mMenuNotifications.SetText(s);
					Debug.Log ("SPAWNPOINT: Wave " + mWavesDone + " complete.");

					// Reset data for the next wave.
					mWaveTimeLeft = waveLength / (float)spawnsPerWave;
					mTimeLeftBetweenWaves = timeBetweenWaves;
					mNumSpawned = 0;
				}
			}
			// Count down to the next spawn.
			else
			{
				mMenuNotifications.SetText("");

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

	bool AreAllEnemiesDead()
	{
		bool result = false;
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemyList.Length == 0)
        {
			result = true;
		}
		return result;
	}
}
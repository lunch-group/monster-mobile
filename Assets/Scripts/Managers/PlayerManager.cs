using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	private ArrayList mPlayers =  new ArrayList();

	private GameObject mActivePlayer = null;
	public GameObject GetActivePlayer() { return mActivePlayer; }
	public void SetActivePlayer(GameObject player)
	{
		if (player != null)
		{
			if (mActivePlayer != null)
			{
				Debug.Log("PLAYERMANAGER: SetActivePlayer to " + player.name + " (from: " + mActivePlayer.name + ")");
			}
			else
			{
				Debug.Log("PLAYERMANAGER: SetActivePlayer to " + player.name + " (from: NULL)");
			}
		}
		else if (mActivePlayer != null)
		{
			Debug.Log("PLAYERMANAGER: SetActivePlayer to NULL (from: " + mActivePlayer.name + ")");
		}
		else
		{
			Debug.Log("PLAYERMANAGER: SetActivePlayer to NULL (from: NULL)");
		}

		mActivePlayer = player;
	}

	// Call this to remove active players, otherwise they may overwrite another that requested to be active.
	public void DeactivatePlayer(GameObject player)
	{
		if (player != null)
		{
			if (mActivePlayer == player)
			{
				Debug.Log("PLAYERMANAGER: DeactivatePlayer: " + player.name);
				mActivePlayer = null;
			}
		}
	}

	void Start ()
	{
		// Get all players in the scene.
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject p in players)
		{
			AddPlayer(p);
		}
	}
	
	void Update ()
	{
	}

	public void AddPlayer(GameObject player)
	{
		mPlayers.Add(player);
	}
}

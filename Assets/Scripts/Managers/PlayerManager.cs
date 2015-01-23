using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
	private ArrayList mPlayers =  new ArrayList();

	private GameObject mActivePlayer = null;
	public GameObject GetActivePlayer() { return mActivePlayer; }
	public void SetActivePlayer(GameObject player) { mActivePlayer = player; }


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

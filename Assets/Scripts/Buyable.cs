using UnityEngine;
using System.Collections;

public class Buyable : MonoBehaviour
{
	public int Cost = 1;

	void Start ()
	{	
	}
	
	void Update ()
	{
	}

	public bool IsAffordable()
	{
		return (Player.gold >= Cost);
	}

	public bool Purchase()
	{
		bool success = false;

		if (Player.gold >= Cost)
		{
			Player.gold -= Cost;
			success = true;

			Debug.Log("BUYABLE: Purchased item for " + Cost + " gold.");
		}

		return success;
	}
}

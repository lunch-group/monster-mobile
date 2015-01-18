using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int health = 10;
	
	// Update is called once per frame
	void Update ()
	{
		// set textmesh text
		TextMesh tm = GetComponentInChildren<TextMesh>();
		tm.text = new string('-', health);
		
		// set textmesh color to red
		tm.renderer.material.color = Color.red;
		
		// adjust health bar so it always faces the camera
		tm.transform.forward = Camera.main.transform.forward;
	}
	
	public void onDeath()
	{        
		// increase player gold
		Player.gold = Player.gold + 1;
		
		// destroy
		Destroy(gameObject); 
	}
}

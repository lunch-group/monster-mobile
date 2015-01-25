using UnityEngine;
using System.Collections;

public class Defensepoint : MonoBehaviour
{	
	public int health = 100;
	
	void OnGUI()
	{		
		// Draw castle health
		GUI.Label(new Rect(0, Screen.height - 30, 100, 30), "Castle Health: " + health);
	}

	void Update()
	{
		// find all teddys
		Enemy[] enemies = (Enemy[])FindObjectsOfType(typeof(Enemy));
		if (enemies != null)
		{
			// find all teddys that are close to the castle
			foreach (Enemy e in enemies)
			{
				float range = Mathf.Max(collider.bounds.size.x, collider.bounds.size.z);
				if (Vector3.Distance(transform.position, e.transform.position) <= range)
				{
					// decrease castle health
					health = health - 1;
					
					// Damage the enemy
					e.TakeDamage(1);
				} 
			}
		}
	}
}
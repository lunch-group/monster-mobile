using UnityEngine;
using System.Collections;

public class Defensepoint : MonoBehaviour
{
	public GUISkin skin = null;
	
	public int health = 100;
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		// draw castle health
		GUI.Label(new Rect(0, 40, 400, 200), "Castle Health: " + health);
	}

	void Update()
	{
		// find all teddys
		Enemy[] teddys = (Enemy[])FindObjectsOfType(typeof(Enemy));
		if (teddys != null)
		{
			// find all teddys that are close to the castle
			for (int i = 0; i < teddys.Length; ++i)
			{
				float range = Mathf.Max(collider.bounds.size.x, collider.bounds.size.z);
				if (Vector3.Distance(transform.position, teddys[i].transform.position) <= range)
				{            
					// decrease castle health
					health = health - 1;
					
					// destroy teddy
					Destroy(teddys[i].gameObject);
				} 
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public class SniperTower : MonoBehaviour
{
	// bullet
	public Bullet bulletPrefab = null;
	
	// interval
	public float interval = 2.0f;
	float timeLeft = 0.0f;
	
	// attack range
	public float range = 10.0f;
	
	// price to build the tower
	public int buildPrice = 1;
	
	// rotation 
	public float rotationSpeed = 2.0f;
	
	Enemy findClosestTarget()
	{
		Enemy closest = null;
		Vector3 pos = transform.position;
		
		// find all teddys
		Enemy[] teddys = (Enemy[])FindObjectsOfType(typeof(Enemy));
		if (teddys != null)
		{
			if (teddys.Length > 0)
			{
				closest = teddys[0];
				for (int i = 1; i < teddys.Length; ++i)
				{
					float cur = Vector3.Distance(pos, teddys[i].transform.position);
					float old = Vector3.Distance(pos, closest.transform.position);
					
					if (cur < old)
					{
						closest = teddys[i];
					}
				}
			}
		}
		
		return closest;
	}
	
	void Update()
	{
		// shoot next bullet?
		timeLeft -= Time.deltaTime;
		if (timeLeft <= 0.0f)
		{
			// find the closest target (if any)
			Enemy target = findClosestTarget();
			if (target != null)
			{        
				// is it close enough?
				if (Vector3.Distance(transform.position, target.transform.position) <= range)
				{
					// spawn bullet
					GameObject g = (GameObject)Instantiate(bulletPrefab.gameObject, transform.position, Quaternion.identity);
					// get access to bullet component
					Bullet b = g.GetComponent<Bullet>();
					// set destination        
					b.setDestination(target.transform);
					
					// reset time
					timeLeft = interval;
				}
			}
		}
		
		// always rotate a bit (animation)
		Vector3 rot = transform.eulerAngles;
		transform.rotation = Quaternion.Euler(rot.x, rot.y + Time.deltaTime * rotationSpeed, rot.z);
	}
}
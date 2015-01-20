using UnityEngine;
using System.Collections;

public class SniperTower : MonoBehaviour
{
	public Projectile ProjectilePrefab = null;
	public float ShotInterval = 2.0f;
	public float Range = 10.0f;

	private float mTimeLeftToShoot = 0.0f;

	
	GameObject FindClosestTargetInRange()
	{
		GameObject closestTarget = null;
		
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");
		if (enemyList.Length > 0)
		{
			closestTarget = enemyList[0];
			for (int i = 1; i < enemyList.Length; ++i)
			{
				float cur = Vector3.Distance(transform.position, enemyList[i].transform.position);
				float old = Vector3.Distance(transform.position, closestTarget.transform.position);
				
				if (cur < old)
				{
					closestTarget = enemyList[i];
				}
			}
		}
		
		return closestTarget;
	}
	
	void Update()
	{
		Buildable buildComp = GetComponentInParent<Buildable>();
		if (buildComp.IsBuilding())
		{
			buildComp.ShowGui(true);
		}
		else if (buildComp.IsDone())
		{
			buildComp.ShowGui(false);

			mTimeLeftToShoot -= Time.deltaTime;
			if (mTimeLeftToShoot <= 0.0f)
			{
				// Find the closest target (if any).
				GameObject target = FindClosestTargetInRange();
				if (target != null)
				{        
					// Is it close enough?
					if (Vector3.Distance(transform.position, target.transform.position) <= Range)
					{
						// Spawn a projectile.
						GameObject g = (GameObject)Instantiate(ProjectilePrefab.gameObject, transform.position, Quaternion.identity);
						// Get the projectile component of the new object.
						Projectile p = g.GetComponent<Projectile>();
						// Set destination of the projectile.
						p.SetDestination(target.transform);
						
						// Reset time to next shot.
						mTimeLeftToShoot = ShotInterval;
					}
				}
			}
		}
	}
}
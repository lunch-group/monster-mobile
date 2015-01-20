using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	public float Speed = 10.0f;
	public int Damage = 1;
	public bool Homing = false;

	private Vector3 mDestination = new Vector3(0.0f, 0.0f, 0.0f);
	
	// Destination set by Tower when creating the bullet.
	private Transform mTargetTransform;


	void Start()
	{
	}

	void Update ()
	{
		// Destroy bullet if target does not exist anymore.
		if (mTargetTransform == null)
		{
			Destroy(gameObject);
			return;
		}

		// Fly towards the destination.
		float stepSize = Time.deltaTime * Speed;
		if (Homing)
		{
			mDestination = mTargetTransform.position;
		}
		transform.position = Vector3.MoveTowards(transform.position, mDestination, stepSize);
		
		// Reached the target?
		if (transform.position.Equals(mDestination))
		{
			Enemy t = mTargetTransform.GetComponent<Enemy>();
			t.TakeDamage(Damage);

			// destroy bullet
			Destroy(gameObject);
		}
	}
	
	public void SetDestination(Transform v)
	{
		mTargetTransform = v;
		mDestination = mTargetTransform.position;
	}
}
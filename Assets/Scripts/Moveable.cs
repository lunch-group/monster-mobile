using UnityEngine;
using System.Collections;

public class Moveable : MonoBehaviour
{
	public Vector3 Destination;
	public float Threshold;
	public bool MoveToPoint;

	public Transform TargetTransform;
	public bool MoveToTransform;

	public bool IsAtTarget;

	public int WarpDelay = 0;
	public bool ShouldWarp = false;


	void Start ()
	{
	}
	
	void Update ()
	{
		if (ShouldWarp)
		{
			if (--WarpDelay <= 0)
			{
				transform.position = Destination;
			}
		}
		else if (MoveToPoint)
		{
			if (transform.position == Destination)
			{
				MoveToPoint = false;
				IsAtTarget = true;
			}
		}
		else if (MoveToTransform && (TargetTransform != null))
		{
			if (Vector3.Distance(transform.position, TargetTransform.position) <= Threshold)
			{
				MoveToTransform = false;
				//mTargetTransform = null;
				IsAtTarget = true;
			}
			else
			{
				NavMeshAgent nav = GetComponent<NavMeshAgent>();
				nav.SetDestination(TargetTransform.position);
			}
		}
	}

	public void RequestMove(Vector3 dest)
	{
		Destination = dest;

		MoveToPoint = true;
		NavMeshAgent nav = GetComponent<NavMeshAgent>();
		nav.SetDestination(Destination);

		MoveToTransform = false;

		IsAtTarget = false;
	}

	public void RequestMove(Transform target)
	{
		if (target != null)
		{
			Destination = transform.position;

			MoveToPoint = false;

			MoveToTransform = true;
			TargetTransform = target;

			IsAtTarget = false;
		}
	}

	public void Warp(int delay)
	{
		ShouldWarp = true;
		WarpDelay = delay;
	}
}

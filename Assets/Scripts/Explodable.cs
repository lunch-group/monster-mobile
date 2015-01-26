using UnityEngine;
using System.Collections;

public class Explodable : MonoBehaviour
{
	public ParticleSystem Effect;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public void PlayEffect()
	{
		if (Effect != null)
		{
			ParticleSystem ps = Instantiate(Effect, transform.position, Quaternion.identity) as ParticleSystem;
			ps.Play();
		}
	}
}

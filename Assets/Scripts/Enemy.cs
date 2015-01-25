using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public int MaxHealth = 10;
	public int Gold = 1;

	private int mHealth = 0;

	void Start()
	{
		mHealth = MaxHealth;
	}

	void Update ()
	{
		if (mHealth <= 0)
		{
			OnDeath ();
		}
		else
		{
			// Display health bar so it always faces the camera
			TextMesh tm = GetComponentInChildren<TextMesh>();
			tm.text = new string('-', mHealth);
			tm.transform.forward = Camera.main.transform.forward;

			Moveable mc = GetComponent<Moveable>();
			if (mc != null)
			{
				if (mc.IsAtTarget)
				{
					Explodable ec = GetComponent<Explodable>();
					if (ec != null)
					{
						GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
						foreach (GameObject pgo in players)
						{
							if (pgo.transform == mc.TargetTransform)
							{
								// Commit suicide
								OnDeath();
							}
						}
					}
				}
			}
		}
	}

	public void TakeDamage(int dmg)
	{
		mHealth -= dmg;
	}
	
	public void OnDeath()
	{
		Player.gold = Player.gold + Gold;

		Explodable ec = GetComponent<Explodable>();
		if (ec != null)
		{
			ec.PlayEffect();
		}

		Destroy(gameObject); 
	}
}

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
			TextMesh tm = GetComponentInChildren<TextMesh>();
			tm.text = new string('-', mHealth);
			// Adjust health bar so it always faces the camera
			tm.transform.forward = Camera.main.transform.forward;
		}
	}

	public void TakeDamage(int dmg)
	{
		mHealth -= dmg;
	}
	
	public void OnDeath()
	{
		Player.gold = Player.gold + Gold;
		
		Destroy(gameObject); 
	}
}

using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
	public int MaxHealth = 1;
	public int CurrentHealth = 0;


	public bool IsDead()
	{
		return (CurrentHealth == 0);
	}

	public void Damage(int dmg)
	{
		CurrentHealth -= dmg;
		if (CurrentHealth < 0)
		{
			CurrentHealth = 0;
		}
	}

	public void Heal(int amount)
	{
		CurrentHealth += amount;
		if (CurrentHealth > MaxHealth)
		{
			Debug.Log (this.gameObject.name + " over-healed by " + (CurrentHealth - MaxHealth).ToString()); 
			CurrentHealth = MaxHealth;
		}
	}
}

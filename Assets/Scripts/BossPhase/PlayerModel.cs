using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
  public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;
    

	void Start()
	{
		healthBar.SetMaxHealth(maxHealth);
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);

		// if (health <= 0)
		// {
		// 	Die();
		// }
	}
}

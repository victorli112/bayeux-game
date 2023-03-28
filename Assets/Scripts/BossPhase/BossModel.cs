using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossModel : MonoBehaviour
{

	public int maxHealth = 500;
	public int currentHealth;
	public HealthBar healthBar;

	void Start()
	{
		healthBar.SetMaxHealth(maxHealth);
		currentHealth = maxHealth;
	}

	void Update()
	{
		// Damage step dealth with in MovingBar.cs
		// if (Input.GetKeyDown(KeyCode.Space))
		// {
		// 	TakeDamage(20);
		// }
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

	// void Die()
	// {
	// 	Instantiate(deathEffect, transform.position, Quaternion.identity);
	// 	Destroy(gameObject);
	// }

}

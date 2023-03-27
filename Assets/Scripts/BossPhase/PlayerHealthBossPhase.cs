using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBossPhase : MonoBehaviour
{
    public int maxHealth = 100;
	public int currentHealth;
	public HealthBar healthBar;
    private System.Random random = new System.Random();

	void Start()
	{
		healthBar.SetMaxHealth(maxHealth);
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && random.Next(0, 100) > 80)
		{
			TakeDamage(20);
		}
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

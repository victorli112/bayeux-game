using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossModel : MonoBehaviour
{

	public int maxHealth = 500;
	public int currentHealth;
	public PlayerModel player;
	public HealthBar healthBar;
	private System.Random random = new System.Random();

	public BossDeathDialog deathDialog;

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

		StartCoroutine(Wrapper());

		if (currentHealth <= 0) {
			deathDialog.StartDialog();
		}
	}

	public IEnumerator Wrapper() {
		yield return StartCoroutine(WaitDamage());
	}

	public IEnumerator WaitDamage() {
        float timeCounter = 0f;
				float timeTotal = 4f;
        while (timeCounter < timeTotal) {
            timeCounter += Time.deltaTime;
            yield return null;
        }
				var dmg = random.Next(5, 20);
				player.TakeDamage(dmg);
    }

	// void Die()
	// {
	// 	Instantiate(deathEffect, transform.position, Quaternion.identity);
	// 	Destroy(gameObject);
	// }

}

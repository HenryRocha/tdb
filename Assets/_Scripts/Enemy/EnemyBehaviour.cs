using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    GameManager gm;

    [SerializeField]
    private int health = 2;

    [SerializeField]
    private GameObject deathEffect;

    // Position of the end of the enemy's path.
    private Transform enemyPathEnd;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Populate the variable.
        enemyPathEnd = GameObject.FindGameObjectWithTag("EnemyPathEnd").GetComponent<Transform>();
        gm = GameManager.GetInstance();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        // If the enemy has reached the end of the path.
        if (transform.position == enemyPathEnd.position) {
            gm.TakeDamage(health);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount) {
        health -= amount;

        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        WaveSpawner.EnemiesAlive--;
        
        gm.EnemyReward(10);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(effect, 1f);
        
        Destroy(gameObject);
    }
}

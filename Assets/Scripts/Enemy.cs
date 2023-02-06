using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    public GameObject HealthCanvas;

    private float slowAmount;
    private float slowDuration;

    private bool alive = true;


    private void Start()
    {
        startSpeed *= Random.Range(1f, 2f);
        speed = startSpeed;
        health = startHealth;
    }

    private void Update()
    {
        if (slowDuration > 0)
        {
            slowDuration -= Time.deltaTime;
            speed = startSpeed * (1 - slowAmount);
        }
        else
        {
            speed = startSpeed;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        HealthCanvas.SetActive(true);

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float amount, float duration)
    {
        slowAmount = amount;
        slowDuration = duration;
    }

    private void Die()
    {
        if (alive)
        {
            alive = false;
            PlayerStats.Money += worth;

            WaveSpawner.EnemiesAlive--;

            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);

            Destroy(gameObject);
        }
    }
}

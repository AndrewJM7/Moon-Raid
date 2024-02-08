using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;

    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chaseSpeed = 2f;

    public Image frontHealthbar;
    public Image backHealthbar;

    [Header("Damage Overlay")]
    public Image damageOverlay; 
    public float opaqueDuration;
    public float fadespeed;

    [Header("Health Overlay")]
    public Image healthOverlay;

    public GameObject player;

    private float durationTimer; 

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0);
        healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < -10)
        {
            TakeDamage(100);
        }

        // Ensures health can't be too high or low
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();
        
        if (damageOverlay.color.a > 0)
        {
            if (health < 30)
            {
                return;
            }

            durationTimer += Time.deltaTime;
            if (durationTimer > opaqueDuration)
            {
                // Damage overlay
                float alpha = damageOverlay.color.a;
                alpha -= Time.deltaTime * fadespeed;
                damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, alpha);
            }
        }

        if (healthOverlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > opaqueDuration)
            {
                // Health overlay
                float alpha = healthOverlay.color.a;
                alpha -= Time.deltaTime * fadespeed;
                healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, alpha);
            }
        }
    }

    public void UpdateHealthBar()
    {
        float fillFront = frontHealthbar.fillAmount;
        float fillBack = backHealthbar.fillAmount;
        float healthFraction = health / maxHealth;

        if (fillFront < healthFraction)
        {
            backHealthbar.color = Color.green;
            backHealthbar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chaseSpeed;
            // Eases animation
            percentComplete = percentComplete * percentComplete;
            frontHealthbar.fillAmount = Mathf.Lerp(fillFront, backHealthbar.fillAmount, percentComplete);
        }

        if (fillBack > healthFraction)
        {
            frontHealthbar.fillAmount = healthFraction;
            backHealthbar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chaseSpeed;
            // Eases animation
            percentComplete = percentComplete * percentComplete;
            backHealthbar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }

        lerpTimer = 0f;

        durationTimer = 0f;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 1);
    }

    public void Heal(float amount)
    {
        health += amount;
        lerpTimer = 0f;

        durationTimer = 0f;
        healthOverlay.color = new Color(healthOverlay.color.r, healthOverlay.color.g, healthOverlay.color.b, 1);
    }

    public void Die()
    {
        // Loads game over scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}

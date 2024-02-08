using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AlienHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private float damageCooldown = 1f;
    private bool isInvincible = false;

    private Animator animator;
    private NavMeshAgent navMeshAgent;

    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chaseSpeed = 2f;

    public Image frontHealthbar;
    public Image backHealthbar;

    public static float counter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthBar();

        if (health <= 0)
        {
            AlienDie();
        }
    }

    public void UpdateHealthBar()
    {
        // Make smooth movement of health bar
        float fillFront = frontHealthbar.fillAmount;
        float fillBack = backHealthbar.fillAmount;
        float healthFraction = health / maxHealth;

        if (fillFront < healthFraction)
        {
            backHealthbar.color = Color.black;
            backHealthbar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chaseSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthbar.fillAmount = Mathf.Lerp(fillFront, backHealthbar.fillAmount, percentComplete);
        }

        if (fillBack > healthFraction)
        {
            frontHealthbar.fillAmount = healthFraction;
            backHealthbar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chaseSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthbar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        // Checks for damage invincibility
        if (!isInvincible)
        {
            if (navMeshAgent != null)
            {
                navMeshAgent.isStopped = true;
            }

            health -= damage;
            animator.SetTrigger("Hit");

            StartCoroutine(DamageCooldown());
        }
    }

    IEnumerator DamageCooldown()
    {
        isInvincible = true;

        // Ensure aliens cant take lots of damage at once
        yield return new WaitForSeconds(damageCooldown);

        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = false;
        }

        isInvincible = false;
    }

    public void AlienDie()
    {
        animator.SetTrigger("Dead");

        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
        }

        Transform aliensParent = transform.parent;

        Destroy(gameObject, 2f);

        // If all aliens are dead
        if (aliensParent != null && aliensParent.childCount == 1)
        {
            GameEnd.AllDead();
        }
    }
}


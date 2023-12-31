using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth = 10f;
    public float moveSpeed = 2f;
    public float def = 20f;
    public float healthPercentage;

    private HealthBar healthBar;
    void Start()
    {
        health = maxHealth;
        healthPercentage = 1;

        healthBar = gameObject.transform.Find("HealthBar").GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        checkIfDead();
    }

    private bool checkIfDead()
    {
        return health <= 0;
    }

    public void takeDamage(float damage)
    {
        health -= damage;

        healthPercentage = health / maxHealth;

        healthBar.AdjustHealth(healthPercentage);


        if (checkIfDead())
        {
            Destroy(gameObject);
        }
    }
}

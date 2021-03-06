﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Projectiles simply move until they hit something or self destruct
/// </summary>
public class Projectile : MonoBehaviour
{
    [SerializeField] float selfDestructTime = 5f;
    [SerializeField] float projectileImpulse = 20f;
    [SerializeField] int projectileDamage = 10;

    // To prevent friendly fire
    public string OwnerTag { get; set; }

    float timer = 0f;
    Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Apply an impulse force as soon as the projectile spawns
        rb.AddForce(transform.right * projectileImpulse, ForceMode2D.Impulse);
        timer = selfDestructTime;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Make sure that enemies don't hit each other
        if ((collision.tag.Equals("Character", System.StringComparison.Ordinal)
            || collision.tag.Equals("Player", System.StringComparison.Ordinal))
            && (!collision.tag.Equals(OwnerTag, System.StringComparison.Ordinal)))
        {
            // Damage the character hit
            collision.gameObject.GetComponent<ACharacter>().TakeDamage(projectileDamage);
            Destroy(this.gameObject);
        }
    }
}

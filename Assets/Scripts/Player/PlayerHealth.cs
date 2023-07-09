using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float immortalityTime = 1f;
    [SerializeField] private float maxHealth = 5f;
    public float Health { get; private set; }
    public bool isDead = false;
    public bool immortality = false;

    private Animator animator;

    public static event Action<float> OnTakeDamage;
    public static event Action<GameObject> OnDead;
    public static event Action PlayerChangedSpawn;

    //public static event Action<float> OnMaxHealthChanged;
    //public static event Action<float> OnHealthChanged;

    private void Start()
    {
        animator = GetComponent<Animator>();
        Health = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        if (immortality)
            return;

        Health -= damage;
        immortality=true;
        if (Health <= 0)
        {
            StartCoroutine(Die());
        }
        StartCoroutine(Immortality());
        animator.SetTrigger("isTakeDamage");
        OnTakeDamage?.Invoke(Health);
    }

    private IEnumerator Die()
    {
        isDead = true;
        OnDead?.Invoke(gameObject);
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(0.8f);
        Respawn();
    }

    public void Kill()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Immortality()
    {
        yield return new WaitForSeconds(immortalityTime);
        immortality = false;
    }
    public void SetMaxHealth(float max)
    {
        maxHealth += max;
        //OnMaxHealthChanged?.Invoke(maxHealth);
    }
    public void Heal(float heal)
    {
        Health += heal;
        if (Health > maxHealth)
        {
            Health = maxHealth;
        }
        //OnHealthChanged?.Invoke(Health);
    }

    #region RESPAWN
    [SerializeField] private Vector2 spawnPoint = Vector2.zero;
    public void Respawn()
    {
        transform.position = spawnPoint;
        Heal(maxHealth);
        animator.SetBool("isDead", false);
        OnTakeDamage?.Invoke(Health);
    }
    public void SetSpawnPoint(Vector2 point)
    {
        spawnPoint = point;
        PlayerChangedSpawn?.Invoke();
    }
    #endregion
}

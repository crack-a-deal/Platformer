using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float damage = 1f;
    [SerializeField] protected float Health;

    
    protected bool isDead;
    
    protected Rigidbody2D rb;
    protected Animator animator;
    protected Patrolling patrolling;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        patrolling = GetComponent<Patrolling>();
    }
    public void TakeDamage(float damage)
    {
        Health-=damage;
        if (Health <= 0)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("isHit");
        }
    }
    protected void Death()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        Destroy(gameObject, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            patrolling.FlipDirection();
        }
    }
}

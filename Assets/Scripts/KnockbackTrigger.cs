using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackTrigger : MonoBehaviour
{
    [SerializeField] private float damage = 0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().Knockback(transform);
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}

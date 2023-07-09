using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    private Animator animator;
    private bool isActive;
    private void Start()
    {
        animator=GetComponent<Animator>();
        PlayerHealth.PlayerChangedSpawn += Active;
    }
    private void Active()
    {
        animator.SetBool("isActive", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().SetSpawnPoint(spawnPoint.position);
            animator.SetBool("isActive", true);
        }
    }
}

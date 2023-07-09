using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    [SerializeField] private float dictanseView=5f;
    [SerializeField] private float slepingCooldown = 5f;
    [SerializeField] private float slepingTimer=0f;
    [SerializeField] private LayerMask playerMask;
    private Vector2 direction;
    private bool isPlayerView = false;

    private void Update()
    {
        SearchPlayer();
        if (isPlayerView)
        {
            //Timer
            slepingTimer -= Time.deltaTime;
            patrolling.StopPatrolling();
            animator.SetBool("isPlayerView",true);
        }
        if (slepingTimer <= 0f)
        {
            isPlayerView = false;
            slepingTimer = 0f;
            animator.SetBool("isPlayerView", false);
            patrolling.StartPatrolling();
        }
    }
    private void SearchPlayer()
    {
        direction = patrolling.isFacingRight ? transform.right : -transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, dictanseView,playerMask);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            isPlayerView = true;
            slepingTimer = slepingCooldown;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * dictanseView);
    }
}

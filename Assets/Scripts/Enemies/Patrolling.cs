using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;

    [SerializeField] private float wallRayDistance = 0.6f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundRayDistance = 1f;
    [SerializeField] private LayerMask groundLayer;

    public bool isFacingRight = false;
    private void Awake()
    {
        GameStateManager.OnGameStateChanged += PatrollingManager;
    }
    private void PatrollingManager(GameState state)
    {
        if (state == GameState.Paused)
            StopPatrolling();
        if (state == GameState.Gameplay)
            StartPatrolling();
    }
    private void FixedUpdate()
    {
        PatrollingArea();
    }
    private void PatrollingArea()
    {
        CheckWalls();
        CheckEdge();
        Vector3 direction = isFacingRight ? transform.right : -transform.right;

        transform.Translate(direction*moveSpeed*Time.deltaTime);
    }
    private void CheckWalls()
    {
        Vector3 raycastDirection = (isFacingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * wallRayDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f, groundLayer);

        if (hit.collider != null)
            FlipDirection();
    }
    private void CheckEdge()
    {
        RaycastHit2D ground = Physics2D.Raycast(groundCheckPosition.position, Vector3.down, groundRayDistance, groundLayer);
        if (ground.collider == null)
            FlipDirection();
    }
    public void FlipDirection()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRayDistance);
        Gizmos.DrawLine(groundCheckPosition.position, groundCheckPosition.position + Vector3.down * groundRayDistance);
    }
    public void StopPatrolling()
    {
        moveSpeed = 0f;
    }
    public void StartPatrolling()
    {
        moveSpeed = 3f;
    }
}

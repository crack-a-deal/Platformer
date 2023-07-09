using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float damage = 5f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private LayerMask attackLayer;

    [SerializeField] private float attackTimer;

    private Animator animator;
    //private PlatformerInputActions platformerInputActions;
    private void Awake()
    {
        //platformerInputActions = new PlatformerInputActions();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        //platformerInputActions.Player.Attack.performed += Attack;
        //platformerInputActions.Player.Attack.Enable();
    }
    private void OnDisable()
    {
        //platformerInputActions.Player.Attack.Disable();
    }
    private void Update()
    {
        attackTimer-=Time.deltaTime;
    }
    private void Attack(InputAction.CallbackContext obj)
    {
        if (attackTimer > 0)
            return;
        attackTimer = attackCooldown;
        animator.SetTrigger("isAttack");

        Collider2D[] targets = Physics2D.OverlapCircleAll(hitPoint.position, attackRadius, attackLayer);
        Enemy enemy;
        Vase vase;
        foreach (Collider2D target in targets)
        {
            if(target.TryGetComponent(out enemy))
            {
                enemy.TakeDamage(damage);
            }
            if(target.TryGetComponent(out vase))
            {
                vase.Breaking();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(hitPoint.position, attackRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector2 dir;

    private bool trigger = false;
    private Animator animator;
    [SerializeField] private bool isFacingRight = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (Random.Range(0, 2) == 0)
            FlipDirection();
    }
    private void Update()
    {
        if (trigger)
        {
            transform.Translate(dir * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isFlying", true);
            trigger = true;
            dir = new Vector2(Random.Range(-1f, 1f), 1);

            if (dir.x > 0 && !isFacingRight)
                FlipDirection();
            if(dir.x<0 && isFacingRight)
            {
                FlipDirection();
            }

            Destroy(gameObject, 8f);
        }
    }
    private void FlipDirection()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight; 
    }
}

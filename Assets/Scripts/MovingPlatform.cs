using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private float movingSpeed = 3f;
    private int currentStep = 0;
    private void Start()
    {
        transform.position = points[0].position;
    }
    private void FixedUpdate()
    {
        if (transform.position == points[currentStep].position)
        {
            currentStep++;
        }
        if (currentStep == points.Length)
        {
            currentStep = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, points[currentStep].position, movingSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}

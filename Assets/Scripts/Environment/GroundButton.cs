using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundButton : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    private SpriteRenderer spriteRenderer;
    public bool Active=false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        spriteRenderer.sprite = on;
        Active = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = off;
        Active = false;
    }
}

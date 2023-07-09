using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject dropItem;

    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Interact()
    {
        animator.SetBool("isOpen", true);
    }
    private void DropLoot()
    {
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<LevelManager>().NextLevel();
    }
}

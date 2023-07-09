using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] private GameObject dropItem;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Breaking()
    {
        animator.SetBool("isBreaking", true);
        Destroy(gameObject, 0.4f);
    }
    public void DropLoot()
    {
        Debug.Log("Drop");
        Instantiate(dropItem, transform.position, Quaternion.identity);
    }
}

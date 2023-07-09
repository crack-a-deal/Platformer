using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GroundButton btn;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (btn.Active)
            animator.SetBool("isOpen", true);
        if(!btn.Active)
            animator.SetBool("isOpen", false);
    }
}

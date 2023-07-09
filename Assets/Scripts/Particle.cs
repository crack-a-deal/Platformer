using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private GameObject pref;
    [SerializeField] private Animator animator;

    public void PlayParticle(string animation, Transform target)
    {
        GameObject particle = Instantiate(pref, target.position, Quaternion.identity);
        animator.Play(animation);
        //StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
}

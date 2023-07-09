using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Material transionMaterial;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private string propertyName = "_Progress";
    public static Action OnTransitionDone;

    private void Start()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        float currentTime = 0f;
        while(currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            transionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime / transitionTime));
            yield return null;
        }
        OnTransitionDone?.Invoke();
        gameObject.SetActive(false);
    }
}

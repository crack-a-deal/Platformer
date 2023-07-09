using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour,IInteractable
{
    [SerializeField] private Sprite off;
    [SerializeField] private Sprite on;
    public bool Active=false;

    public void Interact()
    {
        Active = !Active;
        if (Active)
            gameObject.GetComponent<SpriteRenderer>().sprite = on;
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = off;
    }
}

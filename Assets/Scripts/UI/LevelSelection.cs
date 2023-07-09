using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    private void Start()
    {
        //int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        
        ////Debug.Log(levelAt);
        //for (int i = 0; i < levelButtons.Length; i++)
        //{
        //    if (i >= levelAt)
        //    {
        //        levelButtons[i].interactable = false;
        //    }
        //}
    }
}

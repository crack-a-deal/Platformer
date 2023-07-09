using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //private void Start()
    //{
    //    Debug.Log(PlayerPrefs.GetInt("levelAt"));
    //}
    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
        //if (nextLevel > PlayerPrefs.GetInt("levelAt"))
        //{
        //    PlayerPrefs.SetInt("levelAt", nextLevel);
        //}
    }
    public void PreviousLevel()
    {
        int previousLevel = SceneManager.GetActiveScene().buildIndex - 1;
        SceneManager.LoadScene(previousLevel);
        //if (previousLevel < PlayerPrefs.GetInt("levelAt"))
        //{
        //    PlayerPrefs.SetInt("levelAt", previousLevel);
        //}
    }
    public void LoadLevelById(int id)
    {
        SceneManager.LoadScene(id);
        //PlayerPrefs.SetInt("levelAt", id);
    }
}

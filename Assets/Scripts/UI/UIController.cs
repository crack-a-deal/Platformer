using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private void Awake()
    {
        GameStateManager.OnGameStateChanged += ShowMenu;
    }
    private void ShowMenu(GameState state)
    {
        if (state != GameState.Paused)
            return;

        panel.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetPause()
    {
        GameStateManager.Instance.SetState(GameState.Paused);
    }
    public void SetResume()
    {
        GameStateManager.Instance.SetState(GameState.Gameplay);
    }
}

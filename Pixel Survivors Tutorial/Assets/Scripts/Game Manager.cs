using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputActionReference pause;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        Instance = this;

    }

    void Update()
    {
        if (pause.action.WasPressedThisFrame())
        {
            Pause();
        }
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.gameOverPanel.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
    public void Pause()
    {
        if (UIController.Instance.pausePanel.activeSelf == false && UIController.Instance.gameOverPanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}

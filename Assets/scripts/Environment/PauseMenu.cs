using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool pause = false;
    public Button resumeButton;
    public Button menuButton;
    public Button quitButton;
    public static GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = gameObject;
        canvas.SetActive(false);
        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(Menu);
        quitButton.onClick.AddListener(Exit);
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1;
        pause = false;
        canvas.SetActive(false);
    }
    
    private void Menu()
    {
        pause = false;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    private void Exit()
    {
        pause = false;
        Application.Quit();
    }

}

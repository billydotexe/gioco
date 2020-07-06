using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button play;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        play.onClick.AddListener(Play);
        exit.onClick.AddListener(Exit);
    }

    private void Play()
    {
        SceneManager.LoadScene("test1", LoadSceneMode.Single);
    }

    private void Exit()
    {
        Application.Quit();
    }
}



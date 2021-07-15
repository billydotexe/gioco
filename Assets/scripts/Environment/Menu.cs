using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button play;
    public Button exit;


    public Text highScore;
    public Text lastScore;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        play.onClick.AddListener(Play);
        exit.onClick.AddListener(Exit);
        highScore.text = "0";
        lastScore.text = Manager.score.ToString();
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



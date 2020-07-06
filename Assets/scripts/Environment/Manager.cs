using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    int avgFrameRate;
    static int score;
    static int ammo;
    static bool update = true;
    public Text FPSText;
    public Text scoreText;
    public Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        update = true;
        score = 0;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.pause = !PauseMenu.pause;
            
            if (PauseMenu.pause)
            {
                PauseMenu.canvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                PauseMenu.canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
            }
        
        }

        if (update)
        {
            update = false;
            scoreText.text = "Score: " + score.ToString();
            ammoText.text = "Ammo: " + ammo.ToString();

        }

        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        FPSText.text = avgFrameRate.ToString() + " FPS";

    }

    public static void Death()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public static void Score()
    {
        score++;
        update = true;
    }

    public static void UpdateAmmo(int newAmmo)
    {
        ammo = newAmmo;
        update = true;
    }

}

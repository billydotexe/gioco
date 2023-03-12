using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

    int avgFrameRate;
    public static int score = 0;
    static int ammo;
    static bool updateScore = true;
    static bool drop = false;
    static Vector3 dropPosition;
    public GameObject dropContainer;
    Vector3 offset = new Vector3(0, 0, 0);
    public Text FPSText;
    public Text scoreText;
    public Text ammoText;
    
    public List<GameObject> items;
    public List<int> itemsDropRate;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updateScore = true;
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
                Time.timeScale = 0;
            }
            else
            {
                PauseMenu.canvas.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 1;

            }

        }

        if (updateScore)
        {
            updateScore = false;
            scoreText.text = "Score: " + score.ToString();
            ammoText.text = "Ammo: " + ammo.ToString();

        }

        if(drop)
        {
            drop = false;
            Drop();
            dropPosition = new Vector3(0, 0, 0);//TODO: set to center
        }

        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        FPSText.text = $"{avgFrameRate.ToString()} FPS";

    }

    void Drop(){
        
        for (int i = 0; i < items.Count; i++)
        {
            if(Random.Range(0, itemsDropRate[0]) <= itemsDropRate[i]){
                GameObject droppedItem = Instantiate(items[i], dropPosition+offset, Quaternion.identity) as GameObject;
                droppedItem.transform.parent = dropContainer.transform;
                offset += new Vector3(0, 1, 0);
            }
        }
        offset = new Vector3(0, 0, 0);
    }

    public static void Death()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public static void Score()
    {
        score++;
        updateScore = true;
    }

    public static void UpdateAmmo(int newAmmo)
    {
        ammo = newAmmo;
        updateScore = true;
    }

    public static void spawnItem(Vector3 position){
        dropPosition = position;
        drop = true;
    }

}

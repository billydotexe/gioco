using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject enemy;
    public float delay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawn", 2, delay);
    }

    void spawn()
    {
        if (!PauseMenu.pause)
            Instantiate(enemy, transform.position, gameObject.transform.rotation);
    }

}

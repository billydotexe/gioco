using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedHeart : MonoBehaviour
{
    public int hp = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !Player.isFullHealth) {
            Player.heal(hp);
            Destroy(gameObject);
        }
            
    }
}

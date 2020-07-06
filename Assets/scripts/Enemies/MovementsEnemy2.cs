using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class MovementsEnemy2 : MonoBehaviour
{

    public GameObject player;
    float speed;

    void Start()
    {
        player = GameObject.Find("Player");
        speed = gameObject.GetComponent<Enemy>().speed;
    }

    void FixedUpdate()
    {
        if(!PauseMenu.pause)
            MoveEnemy();
    }

    void MoveEnemy()
    {
        //rotate the enemy to always face the player
        if (player != null)
        {
            Vector2 whereToLook = player.transform.position - transform.position;
            float angle = Mathf.Atan2(whereToLook.y, whereToLook.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
        }
    }

}

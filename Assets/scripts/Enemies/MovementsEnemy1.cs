using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class MovementsEnemy1 : MonoBehaviour
{
    public GameObject player;
    private string[] possibleMovements = { "TOP", "DOWN", "LEFT", "RIGHT", "TOPRIGHT", "TOPLEFT", "BOTTOMRIGHT", "BOTTOMLEFT" };
    private string mov;
    public float speed = 3f;
    private Vector3 movement;

    void Start()
    {
        player = GameObject.Find("Player");
        InvokeRepeating("ChangeDirection", 0, .5f);
    }

    void FixedUpdate()
    {
        if (!PauseMenu.pause)
            MoveEnemy();
    }

    void ChangeDirection()
    {
        mov = possibleMovements[UnityEngine.Random.Range(0, possibleMovements.Length)];
    }

    void MoveEnemy()
    {
        if(player != null)
        {

            Vector2 whereToLook = player.transform.position - transform.position;
            float angle = Mathf.Atan2(whereToLook.y, whereToLook.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (mov == "TOP") movement = Vector3.up;
            else if (mov == "DOWN") movement = Vector3.down;
            else if (mov == "LEFT") movement = Vector3.left;
            else if (mov == "RIGHT") movement = Vector3.right;
            else if (mov == "BOTTOMLEFT") movement = Vector3.down + Vector3.left;
            else if (mov == "BOTTOMRIGHT") movement = Vector3.down + Vector3.right;
            else if (mov == "TOPLEFT") movement = Vector3.up + Vector3.left;
            else if (mov == "TOPRIGHT") movement = Vector3.up + Vector3.right;

            gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + speed * Time.fixedDeltaTime * movement);
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Walls") 
            ChangeDirection();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{

    public float speed;


    public Rigidbody2D player;
    public Camera cam;
    
    Vector2 movement;
    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //get the mouse position
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);


        speed = gameObject.GetComponent<Player>().speed;
    }

    private void FixedUpdate()
    {
        if (!PauseMenu.pause)
            Move();
    }

    private void Move() {
        //move the player
        player.MovePosition(player.position + movement * speed * Time.fixedDeltaTime);   
        //rotate the player with the right angle
        Vector2 whereToLook = mousePosition - player.position;
        float angle = Mathf.Atan2(whereToLook.y, whereToLook.x) * Mathf.Rad2Deg - 90f; 
        player.rotation = angle;
    }
}

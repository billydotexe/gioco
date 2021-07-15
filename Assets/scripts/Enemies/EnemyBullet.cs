using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject player;
    public GameObject hitBullet;
    public float despawnTime = .3f;
    public int damage = 1;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(100f, 0f, 97f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        //the bullets shouldn't hit the player, themselves or other weapons
        if (collision?.attachedRigidbody != null
            &&collision.attachedRigidbody.tag != "Enemy")
        {
            //now let's check if the bullet hit an enemy
            if (collision?.gameObject?.GetComponent<Player>() != null)
            {
                collision.gameObject.GetComponent<Player>().Damage(damage); 
                
            }

            //spawn the animation for the collision and delete it after a certain time
            GameObject hit = Instantiate(hitBullet, transform.position, gameObject.transform.rotation);
            hit.GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.color;
            Destroy(hit, despawnTime);
            //delete istantly the bullet to leave only the animation
            Destroy(gameObject);
        }
    }
}

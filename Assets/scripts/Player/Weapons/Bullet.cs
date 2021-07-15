using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public GameObject hitBullet;
    public float despawnTime = .3f;
    public int damage = 1;

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        
        //the bullets shouldn't hit the player, themselves or other weapons
        if (collision?.attachedRigidbody != null
            && collision.attachedRigidbody.tag == "Enemy" 
            && collision.attachedRigidbody.tag != "Player")
        {
        
            //now let's check if the bullet hit an enemy
            if(collision?.gameObject?.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage);
            }

            //spawn the animation for the collision and delete it after a certain time
            GameObject hit = Instantiate(hitBullet, transform.position, gameObject.transform.rotation);
            hit.GetComponent<Renderer>().material.color = Color.green;
            Destroy(hit, despawnTime);
            //delete istantly the bullet to leave only the animation
            Destroy(gameObject);
        }
    }
}

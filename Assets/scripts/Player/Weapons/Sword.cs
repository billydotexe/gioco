using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D collision) {
        //the sword shouldn't hit the player, themselves or other weapons
        if (collision?.attachedRigidbody != null
            && collision.attachedRigidbody.tag == "Enemy" 
            && collision.attachedRigidbody.tag != "Player")
        {

            if (collision?.gameObject?.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().Damage(damage);

            }
        }
        
    }
}

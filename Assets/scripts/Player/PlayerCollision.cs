using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Weapon" && 
            collision.gameObject.tag != "Player" && 
            collision.gameObject.tag != "Walls")
        {
            if (collision?.gameObject?.GetComponent<Enemy>() != null)
            {
                int damage = collision.gameObject.GetComponent<Enemy>().damage;
                gameObject.GetComponent<Player>().Damage(damage);
            }

        }
    }
}

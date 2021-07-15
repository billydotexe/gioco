using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathAnimation;
    public int damage = 5;
    public float speed = 5f;
    public float range = .5f;
    public int hp;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0) Dead();
    }

    private void Dead()
    {
        Manager.Score();
        Manager.spawnItem(transform.position);
        GameObject deadEnemy = Instantiate(deathAnimation, transform.position, gameObject.transform.rotation);
        Destroy(deadEnemy, .5f);
        Destroy(gameObject);
    }

    public void Damage(int sub)
    {
        if (sub <= 0) sub = 1;
        hp -= sub;
    }
    
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public GameObject deathAnimation;

    public int ammo = 20;
    public int damage = 1;
    public float speed = 1f;
    public float range = .5f;
    public int hp = 6;
    public int hpcap = 6;
    int hpmax = 20;

    public static bool isDead = false;

    public delegate void HealthEvent(int hp, int max);

    public static event HealthEvent OnHealthUpdate;

    void Start()
    {
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    void Update()
    {
        if (hp <= 0) Dead();
    }

    private void Dead()
    {
        Manager.Death();
        GameObject deadPlayer = Instantiate(deathAnimation, transform.position, gameObject.transform.rotation);
        Destroy(deadPlayer, .5f);
        Destroy(gameObject);
    }

    public bool IsFullHealth()
    {
        if (hp == hpcap) return true;
        return false;
    }

    public bool IsMaxHealth()
    {
        if (hpcap == hpmax) return true;
        return false;
    }
    
    public void HpUp(int add)
    {
        int actuallyAdded = (add + 1 % 2) * 2;
        hpcap += add;
        if (hpcap > hpmax) hpcap = hpmax;
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    public void HpUp()
    {
        hpcap += 2;
        if (hpcap > hpmax) hpcap = hpmax;
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    public void Heal(int add)
    {
        hp += add;
        if (hp > hpmax) hp = hpmax;
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    public void Heal()
    {
        hp += 1;
        if (hp > hpmax) hp = hpmax;
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    public void Damage(int sub)
    {
        if (sub > 2) sub = 2;
        hp -= sub;

        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    public void Damage()
    {
        hp -= 1;
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

}

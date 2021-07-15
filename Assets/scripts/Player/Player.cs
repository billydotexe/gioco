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


    public static bool isFullHealth = true;
    public static bool isDead = false;
    static bool updateAmmo = false;
    static bool toHeal = false;
    static int healing = 0;
    public delegate void HealthEvent(int hp, int max);

    public static event HealthEvent OnHealthUpdate;

    void Start()
    {
        if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
    }

    void Update()
    {
        isFullHealth = hp == hpcap;

        if (hp <= 0) Dead();
        
        if(updateAmmo) 
        {
            updateAmmo  = false;
            ammo += 10;
        }
        if(toHeal) 
        {
            toHeal  = false;
            hp += healing;
            healing = 0;
            if (hp > hpcap) hp = hpcap;
            if (OnHealthUpdate != null) OnHealthUpdate(hp, hpcap);
            Debug.Log($"{hp}/{hpcap}");
        }
    }

    private void Dead()
    {
        Manager.Death();
        GameObject deadPlayer = Instantiate(deathAnimation, transform.position, gameObject.transform.rotation);
        Destroy(deadPlayer, .5f);
        Destroy(gameObject);
    }

    public bool IsMaxHealth()
    {
        return (hpcap == hpmax);
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

    public static void heal(int h){
        healing = h;
        toHeal = true;
    }

    public static void addAmmo(){
        updateAmmo = true;
    }

}

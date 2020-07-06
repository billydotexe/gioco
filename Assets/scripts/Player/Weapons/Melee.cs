using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Transform hand;
    public GameObject swordPrefab;
    public float force = 100f;
    public float angle = -120f;
    public float attackDuration = .1f;
    private GameObject swordInstance;
   
    // Update is called once per frame
    void Update()
    {
        if(swordInstance == null && Input.GetButtonDown("Fire2") && !PauseMenu.pause){
            MeleeAttack();
        }
    }

    void MeleeAttack(){
        //spawn the sword as parent of the player
        swordInstance = Instantiate(swordPrefab, hand.position, hand.rotation);
        swordInstance.transform.SetParent(hand);
        SetDamage(swordInstance);
        //sword = swordInstance.GetComponent<Rigidbody2D>();
        Destroy(swordInstance, attackDuration);
    }

    //setting the damage of the sword as the same of the player
    public void SetDamage(GameObject target)
    {
        int damage = gameObject.GetComponent<Player>().damage;
        target.GetComponent<Sword>().damage = damage * 2;
    }

}

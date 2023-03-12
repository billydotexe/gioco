using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Player player;
    public Transform firePoint;
    public GameObject[] bulletPrefabs;
    public GameObject[] bulletDestroy;
    private GameObject bulletPrefab;
    private int num;
    public float bulletForce = 10f;
    public float range = .5f;
    
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        Manager.UpdateAmmo(player.ammo);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !PauseMenu.pause && player.ammo > 0)
        {
            Shoot();
        }

    }

    //shooting logic
    void Shoot(){
        player.ammo--;
        Manager.UpdateAmmo(player.ammo);
        //select one of the posible sprites
        num = Random.Range(0, bulletPrefabs.Length);
        bulletPrefab = bulletPrefabs[num];
        //create the bullet on the scene
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bulletInstance.transform.SetParent(firePoint);
        SetDamage(bulletInstance);
        Rigidbody2D bullet = bulletInstance.GetComponent<Rigidbody2D>();
        //shoot the bullet
        bullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        //delete the bullet after a certain time to add the range effect
        range = gameObject.GetComponent<Player>().range;
        Destroy(bulletInstance, range);
    }

    //setting the damage of the bullet as the same of the player
    public void SetDamage(GameObject target)
    {
        int damage = gameObject.GetComponent<Player>().damage;
        target.GetComponent<Bullet>().damage = damage;
    }

}

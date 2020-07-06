using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject[] bulletPrefabs;
    public GameObject[] bulletDestroy;
    private GameObject bulletPrefab;
    private int num;
    public float bulletForce = 5f;
    float range = .5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Shoot", 2, 2f);
    }

    void Shoot()
    {
        if (!PauseMenu.pause) {
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
            range = gameObject.GetComponent<Enemy>().range;
            Destroy(bulletInstance, range);
        }
    }

    //setting the damage of the bullet as the same of the player
    public void SetDamage(GameObject target)
    {
        int damage = gameObject.GetComponent<Enemy>().damage;
        target.GetComponent<EnemyBullet>().damage = damage;
    }
}

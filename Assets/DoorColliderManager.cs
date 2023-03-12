using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColliderManager : MonoBehaviour
{

    public DoorDirection direction;
    public delegate void DoorColliderDelegate(DoorDirection direction);
    public static event DoorColliderDelegate DoorCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DoorCollider != null) DoorCollider(direction);
    }
}

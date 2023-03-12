using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Room 
{
    private string id { get; set; } = Guid.NewGuid().ToString();
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsClear { get => enemies == null || !enemies.Any(); }
    public List<GameObject> enemies { get; set; }
    public RoomType Type { get; set; }

    public List<GameObject> Items { get; set; } = new List<GameObject>();
}

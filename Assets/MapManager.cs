using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    const int DoorDistance = 2;

    public int Width = 30;
    public int Height = 30;
    public int Rooms = 30;

    public GameObject TopWall;
    public GameObject TopWallWithDoor;
    public GameObject RightWall;
    public GameObject RightWallWithDoor;
    public GameObject BottomWall;
    public GameObject BottomWallWithDoor;
    public GameObject LeftWall;
    public GameObject LeftWallWithDoor;

    public List<GameObject> SpawnableEnemies;

    public GameObject drops;

    System.Random rand;
    Room[,] grid;
    (int x, int y) position;

    public delegate void RepositionPlayerEvent(Vector2 position);
    public static event RepositionPlayerEvent repositionPlayer;

    List<(int, int)> points;
    List<(int, int)> toRemove;
    List<(int, int)> cardinals = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
    List<(int, int)> adjacent = new List<(int, int)>() { (-1, -1), (-1, 0), (-1, 1),
                                                          (0, -1),          (0, 1),
                                                           (1, -1), (1, 0), (1, 1)};

    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();
        InitGrid();
        GenerateLevel();
        showGrid();
        InitRoom();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        DoorColliderManager.DoorCollider += ChangeRoom;
        Enemy.DeathUpdate += EnemyDead;
    }
    private void OnDisable()
    {
        DoorColliderManager.DoorCollider -= ChangeRoom;
        Enemy.DeathUpdate -= EnemyDead;
    }

    void InitGrid()
    {
        grid = new Room[Width, Height];
        points = new List<(int, int)>() { (Width / 2, Height / 2) };

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {

                grid[i, j] = new Room()
                {
                    Type = (i == 0 || j == 0 || i == Width - 1 || j == Width - 1 || i == Height - 1 || j == Width - 1) ? RoomType.Border : RoomType.None
                };
            }
        }
        int x = Width / 2, y = Height / 2;
        grid[x, y].Type = RoomType.Start;
        position = (x, y);
    }

    void InitRoom()
    {
        if (grid[position.x - 1, position.y].Type != RoomType.None
            && grid[position.x - 1, position.y].Type != RoomType.Border)
        {
            // top door
            TopWallWithDoor.SetActive(true);
            TopWall.SetActive(false);
        }
        else
        {
            //top wall
            TopWallWithDoor.SetActive(false);
            TopWall.SetActive(true);
        }
        if (grid[position.x, position.y + 1].Type != RoomType.None
            && grid[position.x, position.y + 1].Type != RoomType.Border)
        {
            // right door
            RightWallWithDoor.SetActive(true);
            RightWall.SetActive(false);
        }
        else
        {
            //right wall
            RightWallWithDoor.SetActive(false);
            RightWall.SetActive(true);
        }
        if (grid[position.x + 1, position.y].Type != RoomType.None
            && grid[position.x + 1, position.y].Type != RoomType.Border)
        {
            // bottom door
            BottomWallWithDoor.SetActive(true);
            BottomWall.SetActive(false);
        }
        else
        {
            //bottom wall
            BottomWallWithDoor.SetActive(false);
            BottomWall.SetActive(true);
        }
        if (grid[position.x, position.y - 1].Type != RoomType.None
            && grid[position.x, position.y - 1].Type != RoomType.Border)
        {
            // left door
            LeftWallWithDoor.SetActive(true);
            LeftWall.SetActive(false);
        }
        else
        {
            //left wall
            LeftWallWithDoor.SetActive(false);
            LeftWall.SetActive(true);
        }
        LoadItems();
        SpawnEnemies(grid[position.x, position.y].enemies);
    }

    void GenerateLevel()
    {
        while ((GetRooms(grid) < Rooms))
        {
            toRemove = new List<(int, int)>();
            if (points.Count == 0) InitGrid();
            foreach ((int x, int y) point in points)
            {
                List<(int, int)> tmp = new List<(int, int)>();
                int max = 2;
                foreach ((int x, int y) cardinal in cardinals)
                {
                    if (GetRooms(grid) >= Rooms) break;
                    if (IsStuck(grid, point.x, point.y) && toRemove.IndexOf(point) == -1)
                    {
                        toRemove.Add(point);
                    }
                    else
                    {
                        double variance = .3f;
                        foreach ((int x, int y) c in adjacent)
                        {
                            if (grid[point.x + c.x, point.y + c.y].Type != RoomType.None
                                && grid[point.x + c.x, point.y + c.y].Type != RoomType.Border)
                            {
                                variance += max / 10;
                            }
                        }
                        if (variance >= .9f) variance = 0.8f;
                        if (grid[point.x + cardinal.x, point.y + cardinal.y].Type == RoomType.None) 
                        {

                            int n = 0;
                            foreach ((int x, int y) cardinal2 in cardinals)
                            {
                                if (grid[point.x + cardinal.x + cardinal2.x, point.y + cardinal.y + cardinal2.y].Type != RoomType.None
                                    && grid[point.x + cardinal.x + cardinal2.x, point.y + cardinal.y + cardinal2.y].Type != RoomType.Border)
                                {
                                    n++;
                                }
                            }

                            if (rand.NextDouble() <= variance / (n * 2.5))
                            {
                                tmp.Add((point.x + cardinal.x, point.y + cardinal.y));
                                grid[point.x + cardinal.x, point.y + cardinal.y].Type = RoomType.Normal;
                                grid[point.x + cardinal.x, point.y + cardinal.y].enemies = AddEnemies();
                                //max = grid[point.x + cardinal.x, point.y + cardinal.y];
                            }
                        }
                    }

                }
                if (tmp.Count > 0) points = tmp;
            }
            toRemove.ForEach(e => points.Remove(e));
        }
    }

    bool IsStuck(Room[,] g, int x, int y)
    {
        List<(int, int)> cardinals = new List<(int, int)>() { (0, 1), (1, 0), (0, -1), (-1, 0) };
        int borders = 0;
        foreach ((int x, int y) c in cardinals)
        {
            if (g[x + c.x, y + c.y].Type != RoomType.None
                && g[x + c.x, y + c.y].Type != RoomType.Border)
            {
                borders++;
            }
        }
        return borders == 4;
    }

    int GetRooms(Room[,] g)
    {
        int r = 0;
        for (int i = 0; i < g.GetLength(0); i++)
        {
            for (int j = 0; j < g.GetLength(1); j++)
            {
                if (g[i, j].Type != RoomType.None
                    && g[i, j].Type != RoomType.Border) r++;
            }
        }
        return r;
    }

    void ChangeRoom(DoorDirection direction)
    {
        if (!grid[position.x, position.y].IsClear) return;

        SaveItems();

        Vector2 newPlayerPosition = new Vector2(0, 0);
        switch(direction)
        {
            case DoorDirection.DoorUp:
                position.x--;
                newPlayerPosition = new Vector2(0, BottomWallWithDoor.transform.position.y + DoorDistance);
                break;
            case DoorDirection.DoorRight:
                position.y++;
                newPlayerPosition = new Vector2(LeftWallWithDoor.transform.position.x + DoorDistance, 0);
                break;
            case DoorDirection.DoorDown:
                position.x++;
                newPlayerPosition = new Vector2(0, TopWallWithDoor.transform.position.y - DoorDistance);
                break;
            case DoorDirection.DoorLeft:
                position.y--;
                newPlayerPosition = new Vector2(RightWallWithDoor.transform.position.x - DoorDistance, 0);
                break;
        }
        InitRoom();
        if (repositionPlayer != null) repositionPlayer(newPlayerPosition);
    }

    public List<GameObject>? AddEnemies()
    {
        int n;
        List<GameObject> enemies = new List<GameObject>();
        for(int i = 0; i < rand.Next(0, 5); i++)
        {
            n = rand.Next(0, SpawnableEnemies.Count);
            enemies.Add(SpawnableEnemies[n]);
        }
        return enemies;
    }

    void SpawnEnemies(List<GameObject> enemies)
    {
        int p = 0;
        Vector3 enemyPos;
        if (enemies == null || enemies.Count == 0) return;
        foreach(GameObject enemy in enemies)
        {
            if (p % 2 == 0)
                enemyPos = new Vector3(0, 8 + p);
            else
                enemyPos = new Vector3(3 + p, 0);
            Instantiate(enemy, new Vector3(p%2, 3 + p), Quaternion.identity);
            p++;
        }
    }

    void LoadItems()
    {
        Debug.Log("Loading items");
        foreach (GameObject i in grid[position.x, position.y].Items)
        {
            i.SetActive(true);
        }
        Debug.Log("Items loaded");
    }
    void SaveItems()
    {
        Debug.Log("Saving items");
        List<GameObject> items = new List<GameObject>();
        foreach (Transform i in drops.transform)
        {
            if (i.gameObject.activeSelf)
            {
                i.gameObject.SetActive(false);
            items.Add(i.gameObject);
            }
        }
        grid[position.x, position.y].Items = items;
        Debug.Log("Items saved");
    }

    public void EnemyDead()
    {
        grid[position.x, position.y].enemies.RemoveAt(0);
    }
    #region debug
    void showGrid()
    {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < grid.GetLength(0); i++)
        {
            for(int j = 0; j < grid.GetLength(1); j++)
            {
                sb.Append(getCell(grid[i, j].Type));
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }
    string getCell(RoomType cell)
    {
        if (cell == RoomType.None || cell == RoomType.Border) return "▮";
        else if (cell == RoomType.Start) return "▯";
        else return " ";
    }
    #endregion
}

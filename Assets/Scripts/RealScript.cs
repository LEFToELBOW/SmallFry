using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.Runtime.CompilerServices;
using static Cinemachine.DocumentationSortingAttribute;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.XR;
using System.Linq;

public class RealScript : MonoBehaviour
{
    public static int[,] map;
    [Range(40, 50)]
    [SerializeField] private int chance;

    [SerializeField] private int width, height;

    [Range(1, 8)]
    [SerializeField] private int iterations;

    [SerializeField] private Tilemap grid;
    [SerializeField] private Tile wall;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject eel;
    [SerializeField] private GameObject pufferfish;
    [SerializeField] private GameObject fish;
    [SerializeField] private GameObject kelp;
    [SerializeField] private GameObject orange;
    [SerializeField] private GameObject adv;
    [SerializeField] private TMP_Text levelText;

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> pups = new List<GameObject>();

    public static int levelCount;

    private int temp;

    private void Start()
    {
        levelCount = 1;
        temp = levelCount;
        Generate();
    }
    private void Update()
    {
        levelText.text = levelCount.ToString();
        if(levelCount != temp) 
        {
            foreach(GameObject item in enemies)
            {
                Destroy(item);
            }
            foreach(GameObject item in pups)
            {
                Destroy(item);
            }
            temp = levelCount;
            grid.ClearAllTiles();
            Generate();
        }
    }
    private void Generate()
    {
        map = new int[width, height];
        System.Random rand = new System.Random();
        Fill();
        for (int i = 0; i < iterations; i++)
            Smooth();
        Draw();
        SearchSpawn(width / 3, height / 3, 0, levelCount);
        SearchSpawn(2, 2, 1, levelCount);
        SearchSpawn(2, 2, 3, levelCount);
        for (int i = 0; i < 5; i++)
        {
            int advanceChance = rand.Next(0, enemies.Count);
            Instantiate(adv, new Vector3(enemies.ElementAt(advanceChance).transform.GetChild(0).transform.position.x, enemies.ElementAt(advanceChance).transform.GetChild(0).transform.position.y), Quaternion.identity);
        }
    }


    private void Fill()
    {

        System.Random rand = new System.Random();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int r = rand.Next(0, 100); 
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 1;
                else if (r < chance)
                    map[x, y] = 1;
                else
                    map[x, y] = 0;
            }
        }
    }
    private void Draw()
    {
        if (map == null) return;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                    grid.SetTile(new Vector3Int(x, y), wall);
            }
        }
    }
    private void Smooth()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int count = Find(x, y);
                if (count > 4)
                    map[x, y] = 1;
                else if (count < 4)
                    map[x, y] = 0;
            }
        }
    }
    private int Find(int gridX, int gridY)
    {
        int count = 0;
        for (int i = gridX - 1; i <= gridX + 1; i++)
        {
            for (int j = gridY - 1; j <= gridY + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if ((i == gridX) && (j == gridY)) continue;
                    count += map[i, j];
                }
                else count++;
            }
        }
        return count;
    }
    private void SearchSpawn(int x, int y, int player, int level)
    {
        for (int valY = y; valY < height - 3; valY++)
        {
            for (int valX = x; valX < width - 3; valX++)
            {
                if(CheckBox(valX, valY) == 0)
                {
                    if(player == 0)
                    {
                        this.player.transform.GetChild(0).position = new Vector3(valX + 1, valY + 1);
                        return;
                    }
                    else if(player == 1)
                    {
                        SpawnEnemies(level, valX, valY);
                    }
                    else
                    {
                        SpawnPowerUps(level, valX, valY);
                    }
                }
            }
        }
    }
    private int CheckBox(int x, int y)
    {
        for(int valX = x; valX <= x + 3; valX++)
        {
            for (int valY = y; valY <= y + 3; valY++)
            {
                if (map[valX, valY] == 1) return 1;
            }
        }
        return 0;
    }


    private void SpawnPowerUps(int level, int x, int y)
    {
        float chance = 2;
        float minus = .25f * level;
        System.Random rand = new System.Random();
        int r = rand.Next(0, 1000);
        int s = rand.Next(0, 100);
        if(minus % 1 == 0)
        {
            chance = chance - minus;
        }
        if(r < chance)
        {
            if(s < 50)
            {
                GameObject prefab = Instantiate(kelp, new Vector3(x, y), Quaternion.identity);
                pups.Add(prefab);
            }
            else
            {
                GameObject prefab = Instantiate(orange, new Vector3(x, y), Quaternion.identity);
                pups.Add(prefab);
            }

        }
    }
    private void SpawnEnemies(int level, int x, int y)
    {
        bool eel = false;
        bool pufferfish = false;
        float chance = 0;
        chance = 8;

        if(level >= 3 && level < 6)
        {
            eel = true;
            chance -= 2;
        }
        else if(level >= 6)
        {
            eel = true;
            pufferfish = true;
            chance -= 4;
        }
        else 
        {
            eel = false;
            pufferfish = false;
        }
        System.Random rand = new System.Random();
        int r = rand.Next(0, 1000);
        if(r <= chance)
        {
            int s = rand.Next(0, 100);
            if (eel == false)
            { 
                GameObject prefab = Instantiate(this.fish, Vector3.zero, Quaternion.identity);
                prefab.transform.GetChild(0).position = new Vector3(x, y);
                enemies.Add(prefab);
            }
            else if (pufferfish == false)
            {
                if (s <= 50)
                {
                    GameObject prefab = Instantiate(this.fish, Vector3.zero, Quaternion.identity);
                    prefab.transform.GetChild(0).position = new Vector3(x, y);
                    enemies.Add(prefab);
                }
                else
                {
                    GameObject prefab = Instantiate(this.eel, Vector3.zero, Quaternion.identity);
                    prefab.transform.GetChild(0).position = new Vector3(x, y);
                    enemies.Add(prefab);
                }    
            }
            else
            {
                if (s <= 33)
                {
                    GameObject prefab = Instantiate(this.fish, Vector3.zero, Quaternion.identity);
                    prefab.transform.GetChild(0).position = new Vector3(x, y);
                    enemies.Add(prefab);
                } 
                else if (s <= 66)
                {
                    GameObject prefab = Instantiate(this.eel, Vector3.zero, Quaternion.identity);
                    prefab.transform.GetChild(0).position = new Vector3(x, y);
                    enemies.Add(prefab);
                }
                else
                {
                    GameObject prefab = Instantiate(this.pufferfish, Vector3.zero, Quaternion.identity);
                    prefab.transform.GetChild(0).position = new Vector3(x, y);
                    enemies.Add(prefab);  
                }
            }
        }
    }
}

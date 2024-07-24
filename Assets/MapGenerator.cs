using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int[,] map;
    public int width;
    public int height;

    [Range(0,100)]
    public int fillPercent;

    void Start()
    {
        GenerateMapData();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateMapData();
        }
    }

    private void OnValidate()
    {
        // This method is called whenever the script is loaded or a value in the Inspector is changed
        GenerateMapData();
    }

    void GenerateMapData()
    {
        AllocateMapMemory();
        FillMapData();
        SmoothenMap();
    }

    void AllocateMapMemory()
    {
        map = new int[width, height];
    }

    void FillMapData()
    {
        if (map == null) return;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i==0 || i==width-1 || j==0 || j==height-1)
                {
                    map[i, j] = 1;
                }
                else
                {
                    int randomNum = Random.Range(0, 100);
                    map[i, j] = (randomNum < fillPercent) ? 1 : 0;
                }
                
            }
        }
    }

    void SmoothenMap()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || i == width - 1 || j == 0 || j == height - 1)
                {
                    continue;
                }
                int wallNeighbors = CountWallNeighbors(i,j);
                if (wallNeighbors > 4)
                {
                    map[i, j] = 1;
                }
                else if (wallNeighbors < 4)
                {
                    map[i, j] = 0;
                }
            }
        }
    }

    int CountWallNeighbors(int i, int j)
    {
        int count = 0;
        if (map[i - 1, j - 1] == 1) count++;
        if (map[i - 1, j] == 1) count++;
        if (map[i - 1, j + 1] == 1) count++;
        if (map[i, j - 1] == 1) count++;
        if (map[i, j + 1] == 1) count++;
        if (map[i + 1, j - 1] == 1) count++;
        if (map[i + 1, j] == 1) count++;
        if (map[i + 1, j + 1] == 1) count++;
        return count;
    }

    void OnDrawGizmos()
    {
        if (map == null) return;

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int colorVal = map[i, j];
                Gizmos.color = (colorVal == 1) ? Color.black : Color.white;
                float xpos = (float)((-width / 2) + 0.5 + i);
                float zpos = (float)((height / 2) + 0.5 - j);
                Vector3 gpos = new Vector3(xpos, 0, zpos);
                Gizmos.DrawCube(gpos, new Vector3(1, 1, 1));
            }
        }        
    }
}

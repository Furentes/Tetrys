using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grid : MonoBehaviour
{
    // The grid
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w, h];
    public static int count = 0;

    // Round Vector to full numbers
    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // Check if group is inside borders
    public static bool insideBorder(Vector2 pos)
    {
        // (x >= 0) and (x < w) and (y >= 0)
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }
    
    // Deletes a row
    public static void deleteRow(int y)
    {
        count++;
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    // Move row one down
    public static void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // For all rows, call decrease row
    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }

    // Check if the row is full
    public static bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // If the row is full, remove it, decrease all above and increase fall speed
    public static void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if(isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                Group.fallSpeed -= (float)0.025;
                --y;
            }
        }
    }
}

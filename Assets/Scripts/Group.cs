using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;
    float lastLeft = 0;
    float lastRight = 0;
    public static float fallSpeed = 1;
    public static bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI overTxt = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().overText;
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            gameOver = true;
            overTxt.text = "GAME OVER";
            Destroy(gameObject);
        }

        Renderer rend;
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        foreach (Transform child in transform)
        {
            print("Foreach loop: " + child);
            rend = child.GetComponent<Renderer>();

            rend.material.color = randomColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if ((Input.GetKey(KeyCode.LeftArrow) && Time.time - lastLeft >= .1))
        {
            transform.position += new Vector3(-1, 0, 0);

            // If position is valid
            if (isValidGridPos())
            {
                // is valid, update grid
                updateGrid();
                lastLeft = Time.time;
            }
            else
            {
                // is invalid, revert
                transform.position += new Vector3(1, 0, 0);
            }
        }
        // Move Right
        else if ((Input.GetKey(KeyCode.RightArrow) && Time.time - lastRight >= .1))
        {
            transform.position += new Vector3(1, 0, 0);

            // If position is valid
            if (isValidGridPos())
            {
                // is valid, update grid
                updateGrid();
                lastRight = Time.time;
            }
            else
            {
                // is invalid, revert
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // If position is valid
            if (isValidGridPos())
                // is valid, update grid
                updateGrid();
            else
                // is invalid, revert
                transform.Rotate(0, 0, 90);
        }
        // Move Downwards and Fall
        else if ((Input.GetKey(KeyCode.DownArrow) && Time.time - lastFall >= .05) ||
                 Time.time - lastFall >= fallSpeed)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            // Not inside order
            if (!Grid.insideBorder(v))
                return false;

            // block in grid cell and not poart of same group
            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;

        }

        return true;
    }

    void updateGrid()
    {
        // Remove old childs from grid
        for(int y = 0; y < Grid.h; ++y)
            for (int x = 0; x < Grid.w; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
        // Add new childs
        foreach(Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Array of groups
    public GameObject[] groups;
    public TextMeshProUGUI score;
    public TextMeshProUGUI overText;
    public bool over = false;
    public float fallSpeed = 1.0f;

    // Constructor (called when game starts)
    void Start()
    {
        Group.fallSpeed = fallSpeed;
        Grid.count = 0;
        score.text = "Score: " + Grid.count;
        // Spawn initial group
        spawnNext();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            // End the game
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.F1))
        {
            // Restart the game
            restart();
        }
    }

    public void spawnNext()
    {
        score.text = "Score: " + Grid.count;
        // Generates random index
        int i = Random.Range(0, groups.Length);
        // Spawn group at current spawner position
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }

    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

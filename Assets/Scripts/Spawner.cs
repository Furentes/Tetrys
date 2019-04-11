using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Array of groups
    public GameObject[] groups;
    public TextMeshProUGUI score;
    public TextMeshProUGUI overText;
    public bool over = false;

    // Constructor (called when game starts)
    void Start()
    {
        // Spawn initial group
        spawnNext();
    }

    public void spawnNext()
    {
        score.text = "Score: " + Grid.count;
        // Generates random index
        int i = Random.Range(0, groups.Length);
        // Spawn group at current spawner position
        Instantiate(groups[i], transform.position, Quaternion.identity);

        if (Group.gameOver) gameOver();
    }

    public void gameOver()
    {
        score.text = "GAME OVER";
    }
}

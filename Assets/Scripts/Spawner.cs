using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Array of groups
    public GameObject[] groups;

    // Constructor (called when game starts)
    void Start()
    {
        // Spawn initial group
        spawnNext();
    }

    public void spawnNext()
    {
        // Generates random index
        int i = Random.Range(0, groups.Length);
        // Spawn group at current spawner position
        Instantiate(groups[i], transform.position, Quaternion.identity);
    }
}

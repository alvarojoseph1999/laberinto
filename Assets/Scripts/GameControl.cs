using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    private string tag;
    [SerializeField]
    private GameObject playerPrefab;

    private GameObject player;
    [SerializeField]
    private GameObject[] spawnPoints;

    [SerializeField]

    private GameObject selectedSpawnPoint;
    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag(tag);

        int rand = Random.Range(0, spawnPoints.Length);

        selectedSpawnPoint = spawnPoints[rand];

        player = Instantiate(playerPrefab, selectedSpawnPoint.transform.position, selectedSpawnPoint.transform.localRotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 50f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private Transform[] levelParts;
    [SerializeField] private PlayerCharacter2D player;

    private Vector3 lastEndPosition;
    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        int startingSpawnLevelParts = 5;
        for (int i = 0; i < startingSpawnLevelParts; i++)
        {
            SpawnLevelPart();
        }

    }

    private void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform lastLevelPartTransform = SpawnLevelPart(lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Vector3 spawnPosition)
    {
        int i = Random.Range(0, 11);
        if (i > 2)
        {
            Transform levelPartTransform = Instantiate(levelParts[0], spawnPosition, Quaternion.identity);
            return levelPartTransform;
        }
        else
        {
            int num = Random.Range(1, levelParts.Length);
            Transform levelPartTransform = Instantiate(levelParts[num], spawnPosition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}


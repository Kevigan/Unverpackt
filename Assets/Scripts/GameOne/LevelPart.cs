//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabCollectable;
    [SerializeField] private GameObject[] prefabObstacles;
    [SerializeField] private Transform[] spawnSpotCollectable;
    [SerializeField] private Transform[] spawnSpotObstacle;
    [SerializeField] private GameObject[] lanternPrefabs;
    [SerializeField] private Transform lanternPos;
    [SerializeField] private GameObject invincibleObject;
    [SerializeField] private Transform invincibleObjectSpawnPoint;
    [SerializeField] private bool LevelPartZero;
    [SerializeField] private bool LevelPartNoObsctacle;

    private void Start()
    {
        if (LevelPartZero)
        {
            PickRandomCollectable();
            SpawnLantern();
            if (!LevelPartNoObsctacle) PickRandomObstacles();
        }
    }

    private void PickRandomCollectable()
    {
        int i = Random.Range(0, 10);
        if (i > 2)
        {
            int i2 = Random.Range(0, prefabCollectable.Length);
            int i3 = Random.Range(0, spawnSpotCollectable.Length);
            var newCollectable = Instantiate(prefabCollectable[i2], spawnSpotCollectable[i3].position, Quaternion.identity);
            newCollectable.transform.parent = gameObject.transform;
        }
    }

    private void SpawnLantern()
    {
        int i = Random.Range(0, 10);
        int i2 = Random.Range(0, 2);
        if (i < 5)
        {
            var newObstacle = Instantiate(lanternPrefabs[i2], lanternPos.position, Quaternion.identity);
            newObstacle.transform.parent = gameObject.transform;
        }
    }

    private void PickRandomObstacles()
    {
        int i = Random.Range(0, 11);
        int i4 = Random.Range(0, 100);
        if (i >= 3)
        {
            int i2 = Random.Range(0, prefabObstacles.Length);
            int i3 = Random.Range(0, spawnSpotObstacle.Length);
            var newObstacle = Instantiate(prefabObstacles[i2], spawnSpotObstacle[i3].position, Quaternion.identity);
            newObstacle.transform.parent = gameObject.transform;
        }
        if (i4 < 3)
        {
            var newInvinvibleObject = Instantiate(invincibleObject, invincibleObjectSpawnPoint.position, Quaternion.identity);
            newInvinvibleObject.transform.parent = gameObject.transform;
        }
    }



    IEnumerator startCountdown()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() && GameManager.Main.GameState == GameState.playing)
        {
            StartCoroutine(startCountdown());
        }
    }
}

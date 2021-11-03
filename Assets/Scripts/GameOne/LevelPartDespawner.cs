using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartDespawner : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<LevelPart>() is LevelPart levelPart)
        {
            Debug.Log("despawn");
            Destroy(levelPart.transform.parent);
        }
    }
}

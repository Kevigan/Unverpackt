using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int xOffeSet;

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x + xOffeSet, transform.position.y, transform.position.z);
    }
}

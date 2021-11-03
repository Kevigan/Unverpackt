using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private ObstacleType obstacleType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player && obstacleType == ObstacleType.jumpBox)
        {
            player.SetYForce(jumpForce);
        }
    }
}

public enum ObstacleType
{
    normalBox,
    jumpBox,
    fakeBox
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (!player.IsInvincible)
            {
                player.StartInvincible();
                Destroy(gameObject);
            }
        }
    }
}

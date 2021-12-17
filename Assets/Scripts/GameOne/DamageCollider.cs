using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        //animator = GetComponent<Animator>();
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCharacter2D>() is PlayerCharacter2D player)
        {
            if (!player.IsInvincible)
            {
                GameManager.Main.ChangeGameState(GameState.Death);
                player.PlayDeathAnimation();
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}

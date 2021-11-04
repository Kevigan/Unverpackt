using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGame2 : MonoBehaviour
{
    private Vector2 touchPosition = Vector2.zero;
    private bool moveAllowed;
    private CollisionDetection collisionDetection;
    // Start is called before the first frame update
    void Start()
    {
        collisionDetection = GetComponent<CollisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput();
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {

                //Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                //if (collisionDetection.col == touchedCollider)
                //{
                //}
                    moveAllowed = true;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y);
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                //moveDirection = Vector2.zero;
                moveAllowed = false;
            }
        }
    }
}

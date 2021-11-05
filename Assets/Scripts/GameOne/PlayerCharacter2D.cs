using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter2D : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator animator;

    private Vector2 _velocity = Vector2.zero;
    public Vector2 Velocity
    {
        get => new Vector2(xForceSet ? setForce.x : _velocity.x, yForceSet ? setForce.y : _velocity.y);
        set => SetForce(value);
    }
    [SerializeField] private float xVelocity = 0f;
    public bool YVelocityIsActive { get; private set; } = true;
    private bool yForceSet, xForceSet = false;
    private Vector2 setForce = new Vector2(0, 0);
    private Vector2 addForce = new Vector2(0, 0);

    private CollisionDetection collisionDetection;


    [SerializeField] private float speed = 5f;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private float gravityMultipier = 5f;
    [SerializeField] private float jumpForce = 1f;

    private int _jumpAmount = 0;
    [SerializeField] private int JumpAmount = 1;

    private Vector2 touchPosition = Vector2.zero;
    private bool moveAllowed;

    private int lastPoint;
    private int changeSpeedPoint = 500;

    private bool _dead = false;
    public bool Dead { get => _dead; set { _dead = value; } }
    // Start is called before the first frame update
    void Start()
    {
        lastPoint = (int)transform.position.x;
        animator = GetComponent<Animator>();
        GameManager.Main.ChangeGameState(GameState.playing);
        CharacterCollision.OnLandedEvent += Onlanded;
        CharacterCollision.OnGroundLeftEvent += OnGroundLeft;
        _jumpAmount = JumpAmount;
        rigid = GetComponent<Rigidbody2D>();
        collisionDetection = GetComponent<CollisionDetection>();
        UIManager.Main.UpdateSpeedMultiplier(speedMultiplier);
        SoundManager.Main.ChangeBackGroundMusic(BackgroundMusic.backgroundJumpAndRUn);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManager.Main.GameState);
        if (GameManager.Main.GameState == GameState.playing)
        {
            TouchInput();
            collisionDetection.HandleCollision();
            PlayAnimations();
            CalculateSpeedMultiplier();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Main.GameState == GameState.playing)
        {
            CalculateXVelocity();
            CalculateYVelocity();
            ApplyVelocity();
            CalculateDistancePoints();
        }
        else  Velocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        CharacterCollision.OnLandedEvent -= Onlanded;
        CharacterCollision.OnGroundLeftEvent -= OnGroundLeft;
    }

    private void CalculateDistancePoints()
    {
        if (transform.position.x - lastPoint >= 1)
        {
            GameManager.Main.HighScore++;
            lastPoint = (int)transform.position.x;
            GameManager.Main.UpdateScoreDistance();
        }
    }

    private void CalculateSpeedMultiplier()
    {
        if (transform.position.x > changeSpeedPoint)
        {
            changeSpeedPoint += 500;
            speedMultiplier += 0.5f;
            UIManager.Main.UpdateSpeedMultiplier(speedMultiplier);
        }
    }

    public void PlayDeathAnimation()
    {
        Dead = true;
        animator.SetBool("Dead", Dead);
    }

    private void PlayAnimations()
    {
        animator.SetFloat("yVelocity", Velocity.y);
        animator.SetFloat("xVelocity", Velocity.x);
    }

    private void OnGroundLeft()
    {
        animator.SetBool("Grounded", false);
    }

    private void Onlanded()
    {
        _jumpAmount = JumpAmount;
        animator.SetBool("Grounded", true);
    }

    public void ApplyVelocity()
    {
        _velocity += addForce;
        //moveDirection += addForce;
        addForce = Vector2.zero;

        _velocity.x = xForceSet ? setForce.x : _velocity.x;
        _velocity.y = yForceSet ? setForce.y : _velocity.y;

        xForceSet = yForceSet = false;

        _velocity.y = YVelocityIsActive ? _velocity.y : 0f;
        rigid.MovePosition(rigid.position + (new Vector2(_velocity.x * speedMultiplier, _velocity.y) * Time.fixedDeltaTime * speed));
    }

    public void CalculateYVelocity()
    {
        if (!collisionDetection.collision.Grounded)
        {
            _velocity.y -= 9.81f * Time.fixedDeltaTime * gravityMultipier;
            if (_velocity.y < -2.5f) _velocity.y = -2.5f;
        }
        else if (_velocity.y < 0)
        {
            _velocity.y = 0;
        }
    }
    public void CalculateXVelocity()
    {
        _velocity.x = xVelocity;
    }
    public void SetForce(Vector2 value)
    {
        setForce = value;
        yForceSet = xForceSet = true;
    }
    public void SetYForce(float newYForce)
    {
        setForce.y = newYForce;
        yForceSet = true;
    }
    public void SetXForce(float newXForce)
    {
        setForce.x = newXForce;
        xForceSet = true;
    }
    public void AddForce(Vector2 value)
    {
        addForce += value;
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                if (collisionDetection.col == touchedCollider)
                {
                    moveAllowed = true;
                }
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

    public void JumpInput()
    {
        if (_jumpAmount >= JumpAmount)
        {
            SetYForce(jumpForce);
            SoundManager.Main.ChooseSound(SoundType.PlayerJump);
            _jumpAmount--;
        }
    }
}

using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;    
    public BoxCollider2D feet;

    public float time = 0.15f;

    private LayerMask throughGround = 10;
    private LayerMask defaultLayer = 0;

    public float speed = 8f;
    public float jumpingPower = 8f;
    public int movementX = 0;
    public float doubleJumpingPower = 40f;


    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(20f,40f);


    public KeyCode jumpUp; 
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode throughButton; 

    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask Wall;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private LayerMask platformSurface;

    public bool hasPressedJump;
    private bool isFacingRight = true;
    private bool lastPressedRight = false;
    public bool doubleJump;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(moveRight))
        {
            lastPressedRight = true;
        }

        if (Input.GetKeyDown(moveLeft))
        {
            lastPressedRight = false;
        }
        
       if (Input.GetKey(moveRight) && Input.GetKey(moveLeft))
        {
            if (lastPressedRight)
            {
                movementX = 1;
            }

            else
            {
                movementX = -1;
            }

        }

        else if (Input.GetKey(moveRight))
        {
            movementX = 1;
        }

        else if (Input.GetKey(moveLeft))
        {
            movementX = -1;
        }



        /*if (Input.GetKeyDown(jumpUp) && IsGrounded())
        {
            hasPressedJump = true;
            doubleJump = true;
        }
        */

        if (Input.GetKeyDown(jumpUp))
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            doubleJump = true;
            }

            if (!IsGrounded() && doubleJump && !WallSliding())
            {
            rb.AddForce(Vector2.up * doubleJumpingPower, ForceMode2D.Impulse);
            doubleJump = false;
            }  
        }

        if (Input.GetKeyDown(throughButton) && IsGrounded())
        {
            playerCollider.gameObject.layer = throughGround;
            StartCoroutine(WaitTime());
        }


        Flip();
        WallSlide();
        WallJump();

        if (isWallJumping)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {

        if (movementX == 1) 
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            movementX = 0;            
        }
         
         else if (movementX == -1) 
        { 
            rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            movementX = 0;           
        }

     
 /*       if (Input.GetKeyDown(jumpUp) && IsGrounded()) 
        {
           hasPressedJump = false;
           rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse); 
        }
       */
        
    }

     bool IsGrounded()
    {
         //return Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer);
         return Physics2D.BoxCast(feet.bounds.center, feet.bounds.size, 0f, Vector2.down, 0.01f, platformSurface);
    }

    private void Flip()
    {
        if (isFacingRight && movementX < 0f || !isFacingRight && movementX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

      private bool WallSliding()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, Wall);
    }

    private void WallSlide()
    {
        if (WallSliding() && !IsGrounded() && movementX != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding= false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(jumpUp) && wallJumpingCounter > 0f)
        {
            isWallJumping= true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDirection);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        playerCollider.gameObject.layer = defaultLayer;
    }
}
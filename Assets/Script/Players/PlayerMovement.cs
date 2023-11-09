using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;    
    public BoxCollider2D feet;

    public float time = 0.25f;

    private LayerMask throughGround = 10;
    private LayerMask defaultLayer = 0;

    public float speed = 8f;
    public float jumpingPower = 8f;
    public int movementX = 0;

    public KeyCode jumpUp; 
    public KeyCode moveRight;
    public KeyCode moveLeft;
    public KeyCode throughButton; 

    [SerializeField] private LayerMask platformSurface;

    private bool isFacingRight = true;
    private bool lastPressedRight = false;
    private bool canJump;

    private void Awake()
    {
        rb.sharedMaterial.friction = 0.5f;
    }

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
       
        if (IsGrounded())
        {
            canJump = true; //when on ground allow player to jump
        }
        
        if (Input.GetKeyDown(jumpUp))
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
                Invoke(nameof(anotherJump), 0.1f); //Calls for a boolean statement to be false. Needs a delay otherwise it just gonna check for ground and check true right away             
                //anotherJump(); // Cannot jump again

            }
            else if (canJump)
            {
                rb.AddForce(Vector2.up * jumpingPower * 1.3f, ForceMode2D.Impulse); //jumping force +added a bit more power, for the jump to look okay
                anotherJump(); // Cannot jump again

            }
        }
        
        if (Input.GetKeyDown(throughButton) && IsGrounded())
        {
            playerCollider.gameObject.layer = throughGround;
            StartCoroutine(WaitTime());
        }
        
        Flip();
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
    }



    private void anotherJump()
    {
        canJump = false;
    }
    public bool IsGrounded()
    {
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

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        playerCollider.gameObject.layer = defaultLayer;
    }
}
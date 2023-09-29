using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerMovement_Force : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 8f;
    public float jumpingPower = 8f;
   


    public int movementX = 0;

    public KeyCode jumpUp; 
    public KeyCode moveRight;
    public KeyCode moveLeft;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

   
    public bool hasPressedJump;
    private bool isFacingRight = true;
   

    bool lastPressedRight = false;


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
                movementX = -1;
        }
        else if (Input.GetKey(moveRight))
        {

            movementX = 1;
        }

        else if (Input.GetKey(moveLeft))
        {
            movementX = -1;

        }



        if (Input.GetKeyDown(jumpUp) && IsGrounded())
        {
            hasPressedJump = true;
        }

        Flip();
    }
    private void FixedUpdate()
    {

        if (movementX == 1) 
        {
            rb.velocity = new Vector2 (speed, rb.velocity.y);
            movementX = 0;
            
        }
         
         else if (movementX == -1) 
        { 
            rb.velocity = new Vector2 (-speed, rb.velocity.y);
            movementX = 0;
           
        }

        
        else if (movementX == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        else
        {
            rb.velocity = new Vector2 (rb.velocity.x, rb.velocity.y);
        }
        

        if (hasPressedJump && IsGrounded()) 
        {
           hasPressedJump = false;
            
            rb.velocity = new Vector2 (rb.velocity.x, jumpingPower);
            
            
        }
       
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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


}

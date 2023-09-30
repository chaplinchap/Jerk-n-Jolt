using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
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
    private bool lastPressedRight = false;


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
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            movementX = 0;            
        }
         
         else if (movementX == -1) 
        { 
            rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            movementX = 0;           
        }

     
        if (hasPressedJump && IsGrounded()) 
        {
           hasPressedJump = false;
           rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);                       
        }
       
        
    }

     bool IsGrounded()
    {
         return Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer);        
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

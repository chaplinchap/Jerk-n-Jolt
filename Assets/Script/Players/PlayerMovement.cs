using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private AudioManager audioManager;
    public AudioSource audioSourceJump;
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;    
    public BoxCollider2D feet;
    public LandingParticles dashParticles;
    private Ghost ghostScript;

    public float time = 0.25f;

    private LayerMask throughGround = 10;
    private LayerMask defaultLayer = 0;
    private LayerMask ghostLayer = 12;

    public float speed = 8f;
    public float jumpingPower = 8f;
    public int movementX = 0;
    public float fastFallSpeed = 35;

    public float maxSpeed = 17.5f;

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

    private void OnEnable() 
    {
        movementX = 0;

    }

    
    void Start()
    {
        LandingParticles dashParticles = gameObject.GetComponent<LandingParticles>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

       ghostScript = gameObject.GetComponent<Ghost>();

    }
    
    void Update()
    {

        KeyInputs();
        
        Flip();

    }
    private void OnCollisionStay2D(Collision2D other)
    {
        //If the player actually hits the platform allow it to check for the surface of the platform
        if(other.collider.CompareTag("Floor"))
        {
            Invoke("CheckFloor",.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Floor"))
        {
            Invoke("CheckFloor", .0f);
        }
    }
    private void CheckFloor()
    {
        //If the player hits the platform it now will check for the area to jump.
        if (IsGrounded())
        {
            canJump = true; //when on ground allow player to jump
        }
    }
    private void FixedUpdate()
    {

        if (movementX == 1)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            //movementX = 0;            
        }

        else if (movementX == -1)
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            //movementX = 0;           
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
    [SerializeField] private GameObject chargebar;
    private void Flip()
    {
        if (isFacingRight && movementX < 0f || !isFacingRight && movementX > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            //Chargebar will flip
            Vector3 chargebarScale = chargebar.transform.localScale;
            chargebarScale.x *= -1f;
            chargebar.transform.localScale = chargebarScale;
            
            if (IsGrounded() && rb.velocity.x >= maxSpeed)
            {
                dashParticles.LandParticles();
            }

            if (IsGrounded() && rb.velocity.x <= -maxSpeed)
            {
                dashParticles.LandParticles();
            }
        }
        
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(time);
        playerCollider.gameObject.layer = defaultLayer;
    }


    private void KeyInputs() 
    {

        if (Input.GetKeyUp(moveRight) || Input.GetKeyUp(moveLeft))
             movementX = 0;

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

        if (!IsGrounded() && Input.GetKey(throughButton))
        {
            rb.AddForce(Vector2.down * fastFallSpeed, ForceMode2D.Force);
            
        }


        if (PlayerCanIgnoreCollision())
        {
            playerCollider.gameObject.layer = throughGround;
            StartCoroutine(WaitTime());
        }

        
        if (GhostCanIgnoreCollision())
        {
          
            playerCollider.gameObject.layer = throughGround;
            StartCoroutine(WaitTimeGhost(time));
        }
        

        if (Input.GetKeyDown(jumpUp))
        {
            if (IsGrounded() && canJump)
            {
                rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
                anotherJump(); // Cannot jump again
                //audioManager.PlaySFX(audioManager.jump);          Old jump sound
                JumpSound();
            }
        }

        if (Input.GetKeyDown(jumpUp))
        {
            if (canJump && !IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpingPower * 1.4f, ForceMode2D.Impulse); //jumping force +added a bit more power, for the jump to look okay
                anotherJump(); // Cannot jump again
                //audioManager.PlaySFX(audioManager.jump);          Old jump sound
                JumpSound();
            }
        }
    }

    public bool GhostCanIgnoreCollision ()
    {
        bool result;

        if (Input.GetKeyDown(throughButton) && IsGrounded() && ghostScript.isGhostPusher)
        {
            result = true;
        }

       else if(Input.GetKeyDown(throughButton) && IsGrounded() && ghostScript.isGhostPuller)
        {
            result = true;
        }

        else
        {
            result = false;
        }
        return result;
    }

    public bool PlayerCanIgnoreCollision()
    {
        bool result;

        if (Input.GetKeyDown(throughButton) && IsGrounded() && !ghostScript.isGhostPusher)
        {
            result = true;
        }

        else if (Input.GetKeyDown(throughButton) && IsGrounded() && !ghostScript.isGhostPuller)
        {
            result = true;
        }

        else
        {
            result = false;
        }
        return result;
    }

    private IEnumerator WaitTimeGhost(float timeGhost)
    {
        yield return new WaitForSeconds(timeGhost);
        playerCollider.gameObject.layer = ghostLayer;
    }
    
    public int GetMovementX()
    {
        return movementX;
    }
    
    void JumpSound()
    {
        audioSourceJump.pitch = Random.Range(1f, 1.5f);
        audioSourceJump.volume = 0.40f;
        audioSourceJump.Play();
    }


}
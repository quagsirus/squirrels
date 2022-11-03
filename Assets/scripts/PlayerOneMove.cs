using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] private LayerMask platformslayerMask;

    public Rigidbody2D rb1;
    public GameObject p1;
    public GameObject acornR1;
    public GameObject acornL1;
    public float jumpForceNormal = 10;
    public float jumpForceBuff = 12;
    private float speed = 6;
    public bool buffOnOrOff1 = false;
    private float moveInput;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;

    public LayerMask whatIsGround;
    SpriteRenderer sprite;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //if buffOnOrOff1 is true (meaning the player is buff)
            //then player will change back to normal, else player turns buff
            if (buffOnOrOff1) { turningNormal1(); }
            else { turningBuff1(); }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (buffOnOrOff1 == true) { hitting(); }
            else { throwing(); }
        }

            //code for jumping, if the player is buff then the jump force is less
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            // if the player is not buff then the jump force will be normal
            if (buffOnOrOff1 == false) 
            { 
                rb1.velocity = Vector2.up * jumpForceNormal;
                animator.SetBool("isJumping", true);
            }
            else 
            { 
                rb1.velocity = Vector2.up * jumpForceBuff; 
            }
            
        }

        // animation handling for jump
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        } else
        {
            animator.SetBool("isJumping", true);
        }
        
    }
    private void FixedUpdate()
    {
        //controls weather the player is grounded or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb1.velocity = new Vector2(moveInput * speed, rb1.velocity.y);
        
        //flips player when moving as moveinput will be 1 or -1
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0){
            Flip();
        }
        
    }
    void turningBuff1()
    {
        //all code for the squence of turning buff
        buffOnOrOff1 = true;
        rb1.gravityScale = 3.0f;
        sprite.color = new Color(1, 0, 0, 1);

    }
    void turningNormal1()
    {
        //all code for the sequence of turing normal
        buffOnOrOff1 = false;
        rb1.gravityScale = 1.0f;
        sprite.color = new Color(1, 1, 1, 1);
    }
    void Flip()
    {
        //code for fliping the sprite when moving left or right
        facingRight =! facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void hitting()
    {
        Debug.LogError("hitting");
    }
    void throwing()
    {
        if (facingRight) { Instantiate(acornR1, transform.position, Quaternion.identity); }
        if (!facingRight) { Instantiate(acornL1, transform.position, Quaternion.identity); }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] private LayerMask platformslayerMask;

    public Rigidbody2D rb1;
    public GameObject p1;
    public GameObject acorn;
    public float jumpForceNormal = 10;
    public float jumpForceBuff1 = 12;
    private float speed1 = 6;
    public bool buffOnOrOff1 = false;
    private float moveInput1;
    private bool facingRight1 = true;
    private bool isGrounded1;
    public Transform groundCheck1;
    public float checkRadius1;

    public LayerMask whatIsGround1;
    SpriteRenderer sprite1;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        sprite1 = GetComponent<SpriteRenderer>();
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
            if (buffOnOrOff1 == true) 
            { 
                hitting(); 
            }
            else 
            { 
                throwing();
            }
        }

            //code for jumping, if the player is buff then the jump force is less
        if (Input.GetKeyDown(KeyCode.W) && isGrounded1)
        {
            // if the player is not buff then the jump force will be normal
            if (buffOnOrOff1 == false) 
            { 
                rb1.velocity = Vector2.up * jumpForceNormal;
                animator.SetBool("isJumping", true);
            }
            else 
            { 
                rb1.velocity = Vector2.up * jumpForceBuff1; 
            }
            
        }

        // animation handling for jump
        if (isGrounded1)
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
        isGrounded1 = Physics2D.OverlapCircle(groundCheck1.position, checkRadius1, whatIsGround1);
        moveInput1 = Input.GetAxisRaw("Horizontal");
        rb1.velocity = new Vector2(moveInput1 * speed1, rb1.velocity.y);
        
        //flips player when moving as moveinput will be 1 or -1
        if (!facingRight1 && moveInput1 > 0) { Flip(); }
        else if (facingRight1 && moveInput1 < 0) { Flip(); }
        
        //will play run animation when running left or right
        // if there is a more efficient way to do this then go ahead - emma :)
        if (moveInput1 == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }
    void turningBuff1()
    {
        //all code for the squence of turning buff
        buffOnOrOff1 = true;
        rb1.gravityScale = 3.0f;
        sprite1.color = new Color(1, 0, 0, 1);

    }
    void turningNormal1()
    {
        //all code for the sequence of turing normal
        buffOnOrOff1 = false;
        rb1.gravityScale = 2f;
        sprite1.color = new Color(1, 1, 1, 1);
    }
    void Flip()
    {
        //code for fliping the sprite when moving left or right
        facingRight1 =! facingRight1;
        transform.rotation = Quaternion.AngleAxis(180 - transform.eulerAngles.y, Vector3.up);
    }
    void hitting()
    {
        // this has been moved to p1Punch script
    }
    //spawns the acorns which have there own logic
    void throwing()
    {
        GameObject newAcorn = Instantiate(acorn, transform.position, Quaternion.identity);
        newAcorn.GetComponent<acornMov>().speed = 20;
    }
}

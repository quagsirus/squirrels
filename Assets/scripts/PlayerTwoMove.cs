using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    [SerializeField] private LayerMask platformslayerMask;

    public Rigidbody2D rb2;
    public GameObject p2;
    public GameObject acorn;
    
    public float jumpForceNormal2 = 10;
    public float jumpForceBuff2 = 12;
    private float speed2 = 6;
    public bool buffOnOrOff2 = false;
    private float moveInput2;
    private bool facingRight2 = true;
    private bool isGrounded2;
    public Transform groundCheck2;
    public float checkRadius2;
    public float attackRate = 2f;
    float nextAttackTime = 0;

    public LayerMask whatIsGround2;
    SpriteRenderer sprite2;

    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        sprite2 = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            //if buffOnOrOff1 is true (meaning the player is buff)
            //then player will change back to normal, else player turns buff
            if (buffOnOrOff2) { turningNormal2(); }
            else { turningBuff2(); }
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (buffOnOrOff2 == true)
            {
                hitting();
            }
            else
            {
                if (Time.time >= nextAttackTime)
                {
                    throwing();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        //code for jumping, if the player is buff then the jump force is less
        if (Input.GetKeyDown(KeyCode.I) && isGrounded2)
        {
            // if the player is not buff then the jump force will be normal
            if (buffOnOrOff2 == false)
            {
                rb2.velocity = Vector2.up * jumpForceNormal2;
                //animator.SetBool("isJumping", true);
            }
            else
            {
                rb2.velocity = Vector2.up * jumpForceBuff2;
            }

        }

        // animation handling for jump
        if (isGrounded2)
        {
            //animator.SetBool("isJumping", false);
        }
        else
        {
            //animator.SetBool("isJumping", true);
        }

    }
    private void FixedUpdate()
    {
        //controls weather the player is grounded or not
        isGrounded2 = Physics2D.OverlapCircle(groundCheck2.position, checkRadius2, whatIsGround2);
        moveInput2 = Input.GetAxisRaw("Horizontaltwo");
        rb2.velocity = new Vector2(moveInput2 * speed2, rb2.velocity.y);

        //flips player when moving as moveinput will be 1 or -1
        if (facingRight2 == false && moveInput2 > 0) { Flip(); }
        else if (facingRight2 == true && moveInput2 < 0) { Flip(); }

        //will play run animation when running left or right
        // if there is a more efficient way to do this then go ahead - emma :)
        if (moveInput2 > 0)
        {
            //animator.SetBool("isRunning", true);
        }
        else if (moveInput2 < 0)
        {
            //animator.SetBool("isRunning", true);
        }
        else
        {
            //animator.SetBool("isRunning", false);
        }
    }
    void turningBuff2()
    {
        //all code for the squence of turning buff
        buffOnOrOff2 = true;
        rb2.gravityScale = 3.0f;
        sprite2.color = new Color(1, 0, 0, 1);

    }
    void turningNormal2()
    {
        //all code for the sequence of turing normal
        buffOnOrOff2 = false;
        rb2.gravityScale = 2.0f;
        sprite2.color = new Color(1, 1, 1, 1);
    }
    void Flip()
    {
        //code for fliping the sprite when moving left or right
        facingRight2 = !facingRight2;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void hitting()
    {
        // this has been moved to p1Punch script
    }
    //spawns the acorns which have there own logic
    void throwing()
    {
        GameObject newAcorn = Instantiate(acorn, transform.position, Quaternion.identity);
        if (facingRight2)
        {
            newAcorn.GetComponent<acornMov>().speed = 20;
        }
    }
}

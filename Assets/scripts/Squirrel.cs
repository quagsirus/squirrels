using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject acorn;
    public float jumpForceNormal = 10;
    public float jumpForceBuff = 12;
    public string movementAxis;
    float speed = 6;
    bool buffOnOrOff = false;
    float moveInput;
    bool facingRight = true;
    bool isGrounded;
    Transform groundCheck;
    public float checkRadius;
    public float attackRate = 2f;
    float nextAttackTime = 0;
    public Vector2 buffHitbox;
    Vector2 smallHitbox;

    public LayerMask whatIsGround;
    BoxCollider2D boxCollider;

    Animator transformer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("Ground Check");
        transformer = transform.Find("transformer").GetComponent<Animator>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        animator.SetBool("isDisconnected", false);
        smallHitbox = boxCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        // animation handling for jump
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

    }
    private void FixedUpdate()
    {
        //controls weather the player is grounded or not
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        moveInput = Input.GetAxisRaw(movementAxis);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //flips player when moving as moveinput will be 1 or -1
        if (!facingRight && moveInput > 0) { Flip(); }
        else if (facingRight && moveInput < 0) { Flip(); }

        //will play run animation when running left or right
        // if there is a more efficient way to do this then go ahead - emma :)
        if (moveInput == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    public void Transform()
    {
        //if buffOnOrOff is true (meaning the player is buff)
        //then player will change back to normal, else player turns buff
        if (buffOnOrOff) { StartCoroutine(turningNormal1()); }
        else { StartCoroutine(turningBuff1()); }
    }

    public void Throw()
    {
        if (buffOnOrOff == true)
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

    public void Jump()
    {
        //code for jumping, if the player is buff then the jump force is less
        if (isGrounded)
        {
            // if the player is not buff then the jump force will be normal
            if (buffOnOrOff == false)
            {
                rb.velocity = Vector2.up * jumpForceNormal;
                animator.SetBool("isJumping", true);
            }
            else
            {
                rb.velocity = Vector2.up * jumpForceBuff;
            }
        }
    }

    IEnumerator turningBuff1()
    {
        //all code for the squence of turning buff
        buffOnOrOff = true;
        transformer.Play("turning_buff");
        yield return new WaitForSeconds(0.4f);
        animator.Play("buff_idle");
        gameObject.transform.Translate(new Vector3(0, -0.25f));
        boxCollider.offset = new Vector2(0.076f, 0.67f);
        boxCollider.size = buffHitbox;

    }
    IEnumerator turningNormal1()
    {
        //all code for the sequence of turing normal
        buffOnOrOff = false;
        transformer.Play("turning_small");
        yield return new WaitForSeconds(0.4f);
        animator.Play("small_idle");
        gameObject.transform.TransformVector(new Vector3(0, 0.25f));
        boxCollider.offset = new Vector2(0, 0.1f);
        boxCollider.size = smallHitbox;
    }
    void Flip()
    {
        //code for fliping the sprite when moving left or right
        facingRight = !facingRight;
        transform.rotation = Quaternion.AngleAxis(180 - transform.eulerAngles.y, Vector3.up);
    }
    void hitting()
    {
        // this has been moved to p1Punch script
    }
    //spawns the acorns which have there own logic
    void throwing()
    {
        animator.Play("small_idlethrow");
        GameObject newAcorn = Instantiate(acorn, transform.position, Quaternion.identity);
        if (facingRight)
        {
            newAcorn.GetComponent<acornMov>().speed = 20;
        }
    }
}

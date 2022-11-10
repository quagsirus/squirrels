using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Squirrel : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject acorn;
    public float jumpForceNormal = 10;
    public float jumpForceBuff = 12;
    public string movementAxis;
    public bool isBlocking;
    float speed = 6;
    bool isBuff = false;
    float moveInput, checkRadius;
    bool facingRight = true;
    bool isGrounded;
    Transform groundCheck, attackPoint;
    public int punchDamage = 30;
    public float attackRate = 2f;
    float nextAttackTime = 0;
    public Vector2 buffHitbox;
    Vector2 smallHitbox;
    public  int currentHeath = 3;

    public LayerMask whatIsGround, enemyLayer;
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
        smallHitbox = boxCollider.size;
        attackPoint = transform.Find("attackPoint");
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
        //if isBuff is true (meaning the player is buff)
        //then player will change back to normal, else player turns buff
        if (isBuff) { StartCoroutine(TurningNormal()); }
        else { StartCoroutine(TurningBuff()); }
    }

    public void Block()
    {
        if (isBuff)
        {
            isBlocking = true;
            animator.SetBool("isBlocking", true);
        }
    }
    public void CancelBlock()
    {
        isBlocking = false;
        animator.SetBool("isBlocking", false);
    }

    public void Throw()
    {
        if (isBuff)
        {
            Punch();
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
        if (isGrounded && !isBuff)
        {
            // if the player is not buff then the jump force will be normal
            if (isBuff == false)
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

    IEnumerator TurningBuff()
    {
        //all code for the squence of turning buff
        transformer.Play("turning_buff");
        yield return new WaitForSeconds(0.4f);
        animator.Play("buff_idle");
        isBuff = true;
        gameObject.transform.Translate(new Vector3(0, -0.25f));
        boxCollider.offset = new Vector2(0.076f, 0.67f);
        boxCollider.size = buffHitbox;

    }
    IEnumerator TurningNormal()
    {
        //all code for the sequence of turing normal
        isBuff = false;
        CancelBlock();
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
    void Punch()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + 1f / attackRate;
            animator.Play("buff_punch");
            Debug.Log("hittting");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 2, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemyLayer == (enemyLayer | (1 << enemy.gameObject.layer)))
                {
                    Debug.Log("we hit " + enemy.name);
                    enemy.GetComponent<enemyStuff>().takenDamage(punchDamage);
                }
            }
        }
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
    public void takenDamage(int damage)
    {
        currentHeath -= damage;
        Debug.Log("ouch");
        // add hurt animation here -------------
        if (currentHeath <= 0)
        {
            death();
        }
    }
    void death()
    {

    }

}

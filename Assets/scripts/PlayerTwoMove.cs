using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;
    Animator animator;
    bool buttonPressed;
    bool isDespawned = true;
    float lastInteractionTime;

    
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
    
    
    
    
    int currentHeath = 3;
    
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDisconnected", true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            squirrel.Transform();
            buttonPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            squirrel.Throw();
            buttonPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            squirrel.Jump();
            buttonPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            squirrel.Block();
            buttonPressed = true;
        }
        else if (squirrel.isBlocking && Input.GetKeyUp(KeyCode.K))
        {
            squirrel.CancelBlock();
        }

        // Despawn player 2 if we stop recieving inputs
        if (Input.GetAxisRaw(squirrel.movementAxis) != 0)
        {
            buttonPressed = true;
        }
        if (buttonPressed)
        {
            if (isDespawned)
            {
                animator.SetBool("isDisconnected", false);
                isDespawned = false;
            }
            lastInteractionTime = Time.time;
        }
        else if (Time.time - lastInteractionTime >= 5 && !isDespawned)
        {
            // Plays despawn animation and disables hitbox + renderer
            animator.SetBool("isDisconnected", true);
            isDespawned = true;
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
    public void takenDamage(int damage)
    {
        currentHeath -= damage;
        Debug.Log("BANAANANA");
        // add hurt animation here -------------
        if (currentHeath <= 0)
        {
            death();
        }
    }
    
    void death()
    {
        //death woop
    }
}

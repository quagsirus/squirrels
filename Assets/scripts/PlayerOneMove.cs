using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;

    
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
    public float attackRate = 2f;
    float nextAttackTime = 0;

    public LayerMask whatIsGround1;
    SpriteRenderer sprite1;

    public Animator animator;

    // Start is called before the first frame update
    
    
    public int currentHeath = 3;

    

    // Start is called before the first frame update
    void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            squirrel.Transform();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            squirrel.Throw();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            squirrel.Jump();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            squirrel.Block();
        }
        else if (squirrel.isBlocking && Input.GetKeyUp(KeyCode.S))
        {
            squirrel.CancelBlock();
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
        if (facingRight1)
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
        //player is now dead
    }
}

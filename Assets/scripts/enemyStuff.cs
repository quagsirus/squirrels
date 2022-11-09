using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStuff : MonoBehaviour
{
    public int maxHeath = 50;
    int currentHeath = 50;
    private bool facingRight = false;
    public float movespeed = 5f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    int acornDamage = 20;
    public float play1pos;
    public float play2pos;
    public float selfpos;
    private float time1 = 0.25f;
    public float minD = 1;
    float nTime = 0;
    float one;
    float two;
    int curtarget = 1;
    int whichWay = -1;
    private int canPunch = -1;
    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask player;
    public int attackDamage = 1;
    public float attackRate = 2f;
    float nextAttackTime = 0;

    // Start is called before the firllst frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHeath = maxHeath;
        target = GameObject.Find("playerOne").transform;
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
        Debug.Log("dead");
        // add death animation here --------------------

        //line of code below should be the last thing in this function
        this.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        positions();
        if (curtarget == 1)
        {
            if ((play1pos > selfpos) && (facingRight == false)) { Flip(); }
            if ((play1pos < selfpos) && (facingRight == true)) { Flip(); }
            if (inRadius(one))
            {
                // add enemy walking animation here ---------
                rb.velocity = new Vector2(whichWay * movespeed, rb.velocity.y);
            }
        }
        if (curtarget == 2)
        {
            if ((play2pos > selfpos) && (facingRight == false)) { Flip(); }
            if ((play2pos < selfpos) && (facingRight == true)) { Flip(); }
            if (inRadius(two))
            {
                // add enemy walking animation here -------------
                rb.velocity = new Vector2(whichWay * movespeed, rb.velocity.y);
            }
        }
        if (inRadius(one) || inRadius(two))
        {
            ePunch();
        }

    }
    void ePunch()
    {
        //add attack animation here --------------------
        Debug.Log("hittting");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, player);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
            enemy.GetComponent<enemyStuff>().takenDamage(attackDamage);
        }
    }
    private void FixedUpdate()
    {
        
    }
    void positions()
    {
        //this long mess gets the x position of player 1 and 2 and finds the closest player
        //and sets that player as there target, this gets run every "time1" seconds curently 6 seconds
        //but feel free to change that :)
        // also feel free to make it look nicer i just cant be bothered at the moment
        if (Time.time >= nTime)
        {
            nTime = Time.time + time1;
            play1pos = GameObject.Find("playerOne").transform.position.x;
            //Debug.Log(play1pos);
            play2pos = GameObject.Find("playerTwo").transform.position.x;
            //Debug.Log(play2pos);
            selfpos = transform.position.x;
            //Debug.Log(selfpos);
            if (selfpos > play1pos)
            {
                one = selfpos - play1pos;
            }
            else
            {
                one = play1pos - selfpos;
            }
            if (selfpos > play2pos)
            {
                two = selfpos - play2pos;
            }
            else
            {
                two = play2pos - selfpos;
            }
            
            if (one > two)
            {
                //Debug.Log("closest to player 2");
                curtarget = 2;
            }
            else
            {
                //Debug.Log("closest to player 1");
                curtarget = 1;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        takenDamage(acornDamage);
    }
    void Flip()
    {
        whichWay *= -1;
        //code for fliping the sprite when moving left or right
        facingRight = !facingRight;
        transform.rotation = Quaternion.AngleAxis(180 - transform.eulerAngles.y, Vector3.up);
    }
    private bool inRadius(float x)
    {
        if (-2 > x || x > 2) { return true; }
        else { return false; }
    }
}

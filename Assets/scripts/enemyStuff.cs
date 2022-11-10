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

    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask playerlayer;
    public int attackDamage = 1;
    float attackRate = 3f;
    float nextAttackTime = 0;

    // Start is called before the firllst frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHeath = maxHeath;
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
        if (Time.time >= nextAttackTime)
        {
            if (inRadius(one) || inRadius(two))
            {
                ePunch();
            }
            nextAttackTime = Time.time + attackRate;
        }
        

    }
    void ePunch()
    {
        //add attack animation here --------------------
        Debug.Log("hittting");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerlayer);
        foreach (Collider2D player in hitPlayers)
        {
            Debug.Log("we hit " + player.name);
            //player.GetComponent<PlayerOneMove>().takenDamage(1);
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

            // Convert to positive number in case of negative
            one = Mathf.Abs(selfpos - play1pos);
            two = Mathf.Abs(selfpos - play2pos);

            // Only target player 2 if it is spawned in and closer
            if (one > two && !GameObject.Find("playerTwo").GetComponent<Animator>().GetBool("isDisconnected"))
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

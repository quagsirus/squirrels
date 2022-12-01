using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class enemyStuff : MonoBehaviour
{
    [SerializeField] Squirrel squirrel1;
    [SerializeField] Squirrel squirrel2;
    public int maxHeath = 50;
    int currentHeath = 50;
    private bool facingRight = false;
    public float movespeed = 5f;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;
    public GameObject barrel;
    public float play1pos;
    public float play2pos;
    public float selfpos;
    private float time1 = 0.25f;
    public float minD = 1;
    float nTime = 0;
    float one;
    float two;
    bool dead;
    int curtarget = 1;
    int whichWay = -1;
    int knockback = 10;


    public Transform attackPoint;
    public float attackRange = 0.6f;
    public LayerMask playerlayer;
    public int attackDamage = 1;
    public float attackRate = 3f;
    float throwRate = 8f;
    float nextAttackTime = 0;
    float nextThrowTime = 0;

    // Start is called before the firllst frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
        currentHeath = maxHeath;
        animator = GetComponent<Animator>();
        switch(Random.Range(0,2))
        {
            case 0:
                //Debug.Log("lol");
                animator.runtimeAnimatorController = Resources.Load("enemy1") as RuntimeAnimatorController;
                break;
            case 1:
                animator.runtimeAnimatorController = Resources.Load("enemy2") as RuntimeAnimatorController;
                break;
        }
        
    }
    public void takenDamage(int damage)
    {
        currentHeath -= damage;
        
        if (currentHeath <= 0)
        {
            StartCoroutine(Die());
        } else
        {
            //Debug.Log("ouch");
            animator.Play("dog_hit");
        }
        if (facingRight)
        {
            rb.velocity = new Vector2(-1 * knockback, rb.velocity.y);
        }
        if (facingRight == false)
        {
            rb.velocity = new Vector2(knockback, rb.velocity.y);
        }
    }
    IEnumerator Die()
    {
        dead = true; 
        //Debug.Log("dead");
        animator.Play("die");
        yield return new WaitForSeconds(0.1f);
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(animationLength);

        //line of code below should be the last thing in this function
        Destroy(gameObject);
        //this.enabled = false;
    }

    void Update()
    {
        if (!dead) {
            positions();
            if (curtarget == 1)
            {
                if ((play1pos > selfpos) && (facingRight == false)) { Flip(); }
                if ((play1pos < selfpos) && (facingRight == true)) { Flip(); }
                if (inRadius(one) == false)
                {
                    animator.SetBool("isWalking", true);
                    rb.velocity = new Vector2(whichWay * movespeed, rb.velocity.y);
                } else
                {
                    animator.SetBool("isWalking", false);
                }
            }
            if (curtarget == 2)
            {
                if ((play2pos > selfpos) && (facingRight == false)) { Flip(); }
                if ((play2pos < selfpos) && (facingRight == true)) { Flip(); }
                if (inRadius(two) == false)
                {
                    animator.SetBool("isWalking", true);
                    rb.velocity = new Vector2(whichWay * movespeed, rb.velocity.y);
                } else
                {
                    animator.SetBool("isWalking", false);
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
            if (Time.time >= nextThrowTime)
            {
                barThrow();
                
                nextThrowTime = Time.time + throwRate;
            }
        }
    }
    void barThrow()
    {
        if (canthrow(one) == false)
        {
            GameObject barrel1 = Instantiate(barrel, transform.position, Quaternion.identity);
            if (facingRight)
            {
                barrel1.GetComponent<barrelRoll>().move = 5;
            }
        }
        

    }
    void ePunch()
    {
        animator.Play("dog_punch");
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerlayer);
        foreach (Collider2D player in hitPlayers)
        {
            if (player.name == "playerOne")
            {
                if (!squirrel1.isBlocking)
                {
                    squirrel1.takenDamage(1, gameObject);
                }
            }
            if (player.name == "playerTwo" && !squirrel2.isDespawned)
            {
                if (!squirrel2.isBlocking)
                {
                    squirrel2.takenDamage(1, gameObject);
                }
            }
        }
        
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
            if (one > two && !GameObject.Find("playerTwo").GetComponent<Animator>().GetBool("isDisconnected") && !squirrel2.isDead)
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
        if (-2 < x && x < 2) { return true; }
        else { return false; }
    }
    private bool canthrow(float x)
    {
        if (-8 < x && x < 8) { return true; }
        else { return false; }
    }
}

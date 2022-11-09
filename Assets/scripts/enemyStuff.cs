using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStuff : MonoBehaviour
{
    public int maxHeath = 50;
    int currentHeath = 50;
    private bool facingRight = false;
    public float movespeed = 5f;
    public GameObject player1;
    public GameObject player2;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    int acornDamage = 20;
    public float play1pos;
    public float play2pos;
    public float selfpos;
    public float time1 = 6f;
    float nTime = 0;
    float one;
    float two;
    int curtarget = 1;
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
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
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
                one = play2pos - selfpos;
            }
            
            if (one > two)
            {
                Debug.Log("playertwo");
                curtarget = 2;
            }
            else
            {
                Debug.Log("closest to player 1");
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
        //code for fliping the sprite when moving left or right
        facingRight = !facingRight;
        transform.rotation = Quaternion.AngleAxis(180 - transform.eulerAngles.y, Vector3.up);
    }
}

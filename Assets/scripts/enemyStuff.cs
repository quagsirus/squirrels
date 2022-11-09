using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStuff : MonoBehaviour
{
    public int maxHeath = 50;
    int currentHeath = 50;
    private bool facingRight = false;
    public float movespeed = 5;
    public GameObject player1;
    public GameObject player2;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    int acornDamage = 20;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHeath = 50;
        target = player1.transform;
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

        //line of code should be the last thing in this function
        this.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

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

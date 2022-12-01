using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;
    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D box;
    bool buttonPressed;
    float lastInteractionTime;
    Transform lives;

    private void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        box = gameObject.GetComponent<BoxCollider2D>();
        animator.SetBool("isDisconnected", true);
        squirrel.isDespawned = true;
        lives = GameObject.Find("p2 lives").GetComponent<Transform>();
    }
    private void Update()
    {
        if (!squirrel.isDead)
        {
            buttonPressed = false;
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
                if (squirrel.isDespawned)
                {
                    animator.SetBool("isDisconnected", false);
                    squirrel.isDespawned = false;
                    rb.gravityScale = 1;
                    box.enabled = true;
                    lives.localScale = new Vector3(1,1,1);
                }
                lastInteractionTime = Time.time;
            }
            else if (Time.time - lastInteractionTime >= 5 && !squirrel.isDespawned)
            {
                // Plays despawn animation and disables hitbox + renderer
                animator.SetBool("isDisconnected", true);
                squirrel.isDespawned = true;
                rb.gravityScale = 0;
                box.enabled = false;
                lives.localScale = new Vector3(0,0,0);
            }
        }
    }
}

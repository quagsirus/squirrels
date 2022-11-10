using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;
    Animator animator;
    bool buttonPressed;
    float lastInteractionTime;

    private void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("isDisconnected", true);
        squirrel.isDespawned = true;
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
            if (squirrel.isDespawned)
            {
                animator.SetBool("isDisconnected", false);
                squirrel.isDespawned = false;
            }
            lastInteractionTime = Time.time;
        }
        else if (Time.time - lastInteractionTime >= 5 && !squirrel.isDespawned)
        {
            // Plays despawn animation and disables hitbox + renderer
            animator.SetBool("isDisconnected", true);
            squirrel.isDespawned = true;
        }
    }
}

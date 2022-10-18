using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] private LayerMask platformslayerMask;

    public Rigidbody2D rb1;
    public GameObject p1;
    public float jumpForceNormal = 10;
    public float jumpForceBuff = 12;
    public bool buffOnOrOff1 = false;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //if buffOnOrOff1 is true (meaning the player is buff)
            //then player will change back to normal, else player turns buff
            if (buffOnOrOff1) { turningNormal1();}
            else { turningBuff1(); }
        }

        //code for jumping, if the player is buff then the jump force is less
        if (Input.GetKeyDown(KeyCode.W))
        {
            // if the player is not buff then the jump force will be normal
            if (buffOnOrOff1 == false) { rb1.velocity = Vector2.up * jumpForceNormal; }
            else { rb1.velocity = Vector2.up * jumpForceBuff; }
        }
    }
    void turningBuff1()
    {
        //all code for the squence of turning buff
        buffOnOrOff1 = true;
        rb1.gravityScale = 3.0f;
        sprite.color = new Color(1, 0, 0, 1);

    }
    void turningNormal1()
    {
        //all code for the sequence of turing normal
        buffOnOrOff1 = false;
        rb1.gravityScale = 1.0f;
        sprite.color = new Color(1, 1, 1, 1);
    }
}

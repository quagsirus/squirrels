using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    public Rigidbody2D rb1;
    public float jumpForceNormal = 10;
    public float jumpForceBuff = 6;
    public GameObject p1;
    public bool buffOnOrOff1 = false;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            buffOnOrOff1 = true;
        }

            //code for jumping, if the player is buff then the jump force is less
            if (Input.GetKeyDown(KeyCode.W))
        {
            if (buffOnOrOff1 == false)
            {
                rb1.velocity = Vector2.up * jumpForceNormal;
            }
            else
            {
                rb1.velocity = Vector2.up * jumpForceBuff;
            }
            

        }
    }
}

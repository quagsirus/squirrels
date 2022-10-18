using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMove : MonoBehaviour
{
    public Rigidbody2D rb2;
    public float jumpForce = 10;
    public GameObject p2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            rb2.velocity = Vector2.up * jumpForce;
            
        }
    }
}

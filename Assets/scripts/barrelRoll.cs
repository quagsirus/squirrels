using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelRoll : MonoBehaviour
{
    [SerializeField] [HideInInspector] Squirrel squirrel1;
    [SerializeField] [HideInInspector] Squirrel squirrel2;
    Rigidbody2D rb;
    public int move = -5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        squirrel1 = GameObject.Find("playerOne").GetComponent<Squirrel>();
        squirrel2 = GameObject.Find("playerTwo").GetComponent<Squirrel>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(move, rb.velocity.y);
    }
    public void OnTriggerEnter2D(Collider2D player)
    {
        
        if (player.gameObject.tag == "Player")
        {
            if (player.name == "playerOne")
            {
                squirrel1.takenDamage(1, gameObject);
                Destroy(gameObject);
            }
            if (player.name == "playerTwo" && !squirrel2.isDespawned)
            {
                squirrel2.takenDamage(1, gameObject);
                Destroy(gameObject);
            }
            
        }
        if (player.gameObject.tag == "wall") 
        {
            Destroy(gameObject);
        }
        
    }
    
}

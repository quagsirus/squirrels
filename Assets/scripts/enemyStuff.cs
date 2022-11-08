using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStuff : MonoBehaviour
{
    public int maxHeath = 50;
    int currentHeath;
    // Start is called before the first frame update
    void Start()
    {
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

        //line of code should be the last thing in this function
        this.enabled = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
    }
}

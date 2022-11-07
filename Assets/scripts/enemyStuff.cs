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
        
    }
    public void takenDamage(int damage)
    {
        currentHeath -= damage;
        // add hurt animation here -------------
        if (currentHeath <= 0)
        {
            //death... to be added soon
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        currentHeath = maxHeath;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
    }
}

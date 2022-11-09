using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornMov : MonoBehaviour
{
    public GameObject acorn;
    
    public Rigidbody2D rbac;
    public float speed = -20;
    public float upl = 2;
    public int acornDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        rbac = GetComponent<Rigidbody2D>();
        rbac.velocity = new Vector2(rbac.velocity.x, upl);
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {
        rbac.velocity = new Vector2(speed, rbac.velocity.y);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        //please someone call the function takenDamage(acornDamage) from enemyStuff and apply
        //it to the enemy it has hit
        Destroy(gameObject);
    }
}

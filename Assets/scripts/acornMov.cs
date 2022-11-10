using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornMov : MonoBehaviour
{
    public GameObject acorn;
    
    public Rigidbody2D rbac;
    public float speed = -20;
    float upl = 5;
    public int acornDamage = 0;
    public LayerMask enemyLayer;

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
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemyLayer == (enemyLayer | (1 << enemy.gameObject.layer)))
        {
            enemy.GetComponent<enemyStuff>().takenDamage(acornDamage);
            Destroy(gameObject);
        }
    }
}

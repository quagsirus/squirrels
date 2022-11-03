using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornRMov : MonoBehaviour
{
    public GameObject acornR;
    public Rigidbody2D rbacR;
    public float speedR = 20;
    public float uplR = 2;
    // Start is called before the first frame update
    void Start()
    {
        rbacR = GetComponent<Rigidbody2D>();
        rbacR.velocity = new Vector2(rbacR.velocity.x, uplR);
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FixedUpdate()
    {
        rbacR.velocity = new Vector2(speedR, rbacR.velocity.y);
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

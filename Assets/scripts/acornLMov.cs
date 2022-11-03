using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class acornLMov : MonoBehaviour
{
    public GameObject acornL;
    public Rigidbody2D rbacL;
    public float speedL = -20;
    public float uplL = 2;
    // Start is called before the first frame update
    void Start()
    {
        rbacL = GetComponent<Rigidbody2D>();
        rbacL.velocity = new Vector2(rbacL.velocity.x, uplL);
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FixedUpdate()
    {
        rbacL.velocity = new Vector2(speedL, rbacL.velocity.y);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}

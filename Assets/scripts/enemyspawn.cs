using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //GameObject newEnemy = Instantiate(enemy, self.transform);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GameObject newEnemy = Instantiate(enemy, self.transform);
        }
    }
    IEnumerator spawn()
    {
        
        yield return new WaitForSeconds(10);
        GameObject newEnemy = Instantiate(enemy, self.transform);
        StartCoroutine(spawn());

    }
}

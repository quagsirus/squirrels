using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject self;
    private int tTime = 10;
    private int tally = 0;
    // Start is called before the first frame update
    void Start()
    {
        tTime = 10;
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
        
        GameObject newEnemy = Instantiate(enemy, self.transform);
        yield return new WaitForSeconds(tTime);
        tally++;
        if (tally > 2)
        {
            tTime--;
            tally = 0;
        }
        StartCoroutine(spawn());

    }
}

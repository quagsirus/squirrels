using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Punch : MonoBehaviour
{
    private int canPunch = -1;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F)) { canPunch *= -1; }

        if (Input.GetKeyDown(KeyCode.G) && canPunch == 1)
        {
            punch();
        }
        
    }
    void punch()
    {
        //add attack animation here --------------------
        Debug.Log("hittting");
    }
}

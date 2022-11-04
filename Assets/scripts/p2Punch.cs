using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Punch : MonoBehaviour
{
    private int canPunch2 = -1;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Semicolon)) { canPunch2 *= -1; }

        if (Input.GetKeyDown(KeyCode.BackQuote) && canPunch2 == 1)
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

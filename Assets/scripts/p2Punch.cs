using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Punch : MonoBehaviour
{
    private int canPunch2 = -1;
    public Transform attackPoint2;
    public float attackRange2 = 0.6f;
    public LayerMask enemyL2;
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
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyL2);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit " + enemy.name);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if(attackPoint2 == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint2.position, attackRange2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;

    private void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
    }
    private void Update()
    {
        if (!squirrel.isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                squirrel.Transform();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                squirrel.Throw();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                squirrel.Jump();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                squirrel.Block();
            }
            else if (squirrel.isBlocking && Input.GetKeyUp(KeyCode.S))
            {
                squirrel.CancelBlock();
            }
        }
    }
}

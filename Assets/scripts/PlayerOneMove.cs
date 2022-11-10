using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneMove : MonoBehaviour
{
    [SerializeField] Squirrel squirrel;

    private void Start()
    {
        squirrel = gameObject.GetComponent<Squirrel>();
    }
    private void Update()
    {
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //code taken from a youtube tutorial:
    //Smooth Camera Follow in Unity - Tutorial | Brackeys (2017)
    //Brackeys (2017) Smooth Camera Follow in Unity - Tutorial (online video) https://www.youtube.com/watch?v=zit45k6CUMk Accessed at: 3/11/22

    public Transform Player;
    public float speed;
    public Vector3 offset;

    private void Awake()
    {
        speed = 0.125f;
    }
    private void LateUpdate()
    {
        //Vector3 targetPosition = Player.position + offset;
        //Vector3 smoothTransition = Vector3.Lerp(transform.position, targetPosition, speed);
        //transform.position = smoothTransition;
        //transform.position = Player.position + offset;
        transform.position = Player.position;
    }
}

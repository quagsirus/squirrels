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
    public Vector3 minPoint, maxPoint;

    private void Awake()
    {
        speed = 10f;
    }
    private void FixedUpdate()
    {
        Vector3 targetPosition = Player.position + offset;
        //Vector3 smoothTransition = Vector3.Lerp(transform.position, targetPosition, speed);
        //transform.position = smoothTransition;
        //transform.position = Player.position + offset;
        //transform.position = Player.position;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minPoint.x, maxPoint.x),
            Mathf.Clamp(targetPosition.y, minPoint.y, maxPoint.y),
            Mathf.Clamp(targetPosition.z, minPoint.z, maxPoint.z));

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundPosition, speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
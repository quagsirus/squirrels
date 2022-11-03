using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    //code taken from a youtube tutorial:
    //Unity Parallax Tutorial - How to infinate scrolling background | Dani (2019)
    //Dani (2019) Unity Parallax Tutorial - How to infinate scrolling background (online video) https://www.youtube.com/watch?v=zit45k6CUMk Accessed at: 3/11/22

    private float length, startPosition;
    public GameObject Camera;
    public float ParallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (Camera.transform.position.x * (1 - ParallaxEffect));
        float dist = (Camera.transform.position.x * ParallaxEffect);
        transform.position = new Vector3(startPosition + dist, transform.position.y, transform.position.z);

        if (temp > startPosition + length) startPosition += length;
        else if (temp < startPosition - length) startPosition -= length;

    }
}

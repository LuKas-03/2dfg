using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // camera scale's settings
    public float xMax=32;   // public bool xMaxEnabled=true;
    public float xMin=-32;  // public bool xMinEnabled = true;
    public float yMax = 16; // public bool yMaxEnabled = true;
    public float yMin=-16;  // public bool yMinEnabled = true;

    // follower's settings
    public GameObject objectToFollow;
    public float speed = 2.0f;

    // finds the objectToFollow on the screen
    void Update()
    {
        float interpolation = speed * Time.deltaTime;

        Vector3 position = transform.position;

        position.y = Mathf.Clamp(Mathf.Lerp(transform.position.y, objectToFollow.transform.position.y, interpolation), yMin, yMax);
        position.x = Mathf.Clamp(Mathf.Lerp(transform.position.x, objectToFollow.transform.position.x, interpolation), xMin, xMax);

        transform.position = position;
    }

}

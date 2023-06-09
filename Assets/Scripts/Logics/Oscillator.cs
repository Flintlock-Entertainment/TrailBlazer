using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Tooltip("Movement speed in meters per second")]
    [SerializeField]
    float objectSpeed;
    [SerializeField]
    float motionLimit;
    [SerializeField]
    float xOffSet;
    [SerializeField]
    float yOffSet;
    [SerializeField]
    bool leftRight = true;
    float xTime;
    float yTime;
    float zTime;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.x);
        xTime = transform.position.x;
        yTime = transform.position.y;
        zTime = transform.position.z;
        xOffSet = transform.position.x;
        yOffSet = transform.position.y; ;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");

        if (leftRight)
        {
            xTime += Time.deltaTime;
            yTime = transform.position.y;
            transform.position = new Vector3(Mathf.Sin(xTime * objectSpeed) * motionLimit + xOffSet, yTime, zTime); // osilate the object left and right per frame * the speed and the motion limit.
        }
        else
        {
            xTime = transform.position.x;
            yTime += Time.deltaTime;
            transform.position = new Vector3(xTime, Mathf.Sin(yTime * objectSpeed) * motionLimit + yOffSet, zTime); // osilate the object up and down per frame * the speed and the motion limit.
        }

    }
}
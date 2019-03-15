using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLaserScript : MonoBehaviour
{
    int Speed = 5;

    private void OnEnable()
    {
    }

    void Update()
    {
        transform.position = new Vector3(PingPong(Time.time * Speed, -7, 7), 0, 0);
    }

    float PingPong(float t, float minLength, float maxLength)
    {
        return Mathf.PingPong(t, maxLength - minLength) + minLength;
    }

    
}

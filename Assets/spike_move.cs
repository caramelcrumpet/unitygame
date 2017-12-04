using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike_move : MonoBehaviour
{

    Vector3 current_position;
    float direction = 1.0f;
    float speed = 1.5f;
    float heightlimit = 0.8f;
    float timecount = 0.0f;
    float timelimit = 2.5f;

    void Start()
    {
        current_position = this.transform.position;
    }

    void Update()
    {

        transform.Translate(0, direction * speed * Time.deltaTime * 1, 0);


        if (transform.position.y > current_position.y + heightlimit)
        {
            direction = -1;
        }
        if (transform.position.y < current_position.y)
        {
            direction = 0;
            timecount = timecount + Time.deltaTime;

            if (timecount > timelimit)
            {
                direction = 1;
                timecount = 0;
            }
        }
    }
}
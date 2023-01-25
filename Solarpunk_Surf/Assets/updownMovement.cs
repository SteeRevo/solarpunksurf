using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updownMovement : MonoBehaviour
{
    bool rising = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rising)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f * Time.deltaTime, transform.position.z);
            if(transform.position.y > 0.15){
                rising = false;
            }
        }
        else if(!rising)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f * Time.deltaTime, transform.position.z);
            if(transform.position.y < -0.05)
            {
                rising = true;
            }
        }
    }
}

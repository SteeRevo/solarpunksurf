using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;

    public float rotationSpeed = 12f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //easy way
        // transform.LookAt(target);
        // the second argument, upwards, defaults to Vector3.up
        
        Vector3 relativePos = target.position - transform.position;
        /*Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        */

        // only rotate around z
        float targetAngle = Quaternion.LookRotation(relativePos).eulerAngles.z;
        Debug.Log(targetAngle);
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        Debug.Log(angle);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}

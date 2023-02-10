using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public float targetRange = 30;
    public float rotationSpeed = 2f;
    
    public Transform firePosition;
    public GameObject projectile;
    public float delay;
    
    //public AudioSource fireSound;

    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //easy way
        // transform.LookAt(target);
        // the second argument, upwards, defaults to Vector3.up
        
        Vector3 targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 relativePos = targetPos - transform.position;
        if (relativePos.magnitude < targetRange)
        {
            Quaternion lookRotation = Quaternion.LookRotation(relativePos, Vector3.up);

            // The step size is equal to speed times frame time.
            float singleStep = rotationSpeed * Time.deltaTime;
            // Rotate the forward vector towards the target direction by one step
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, relativePos, singleStep, 0.0f);
            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            time += Time.deltaTime;
            if (time >= delay)
            {
                time = time - delay;
                //fireSound.Play();
                Instantiate(projectile, firePosition.position, transform.rotation);
            }
        }
    }
}

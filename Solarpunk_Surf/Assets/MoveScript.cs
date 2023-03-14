using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour
{

    private Vector3 moveDirection = Vector3.forward;
    public float maxSpeed = 20f;
    private float moveSpeed = 0f;
    private bool initRotate = false;
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        var rot = Quaternion.Euler(0, -90, 0);
        moveSpeed += 0.5f;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        transform.position -= moveDirection * Time.deltaTime * moveSpeed;       
        if(initRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 40f * Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("got hit");            
        }   
        if(other.gameObject.tag == "RotateTrigger")
        {
            Debug.Log("Rotate trigger");
            RotateMon();
        }
        if(other.gameObject.tag == "Destructable")
        {
            Debug.Log("destroyed building");
            Destroy(other.gameObject);
        }
    }


    private void RotateMon()
    {
        initRotate = true;
        moveDirection = Vector3.left;
    }
}

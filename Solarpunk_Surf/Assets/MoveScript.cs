using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour
{

    private Vector3 moveDirection = Vector3.forward;
    public float maxSpeed = 20f;
    private float moveSpeed = 0f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        moveSpeed += 0.5f;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        transform.position -= moveDirection * Time.deltaTime * moveSpeed;       
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
    }


    private void RotateMon()
    {
        transform.rotation = Quaternion.Euler(0, -90, 0);
        moveDirection = Vector3.left;
    }
}

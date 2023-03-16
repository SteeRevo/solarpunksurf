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
    private Vector3 rotationVec = new Vector3 (0, 0, 0);
    

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion rot = Quaternion.Euler(rotationVec);
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
            Debug.Log(other.gameObject.GetComponent<RotateTrigger>().getTurnDir());
            if(other.gameObject.GetComponent<RotateTrigger>().getTurnDir() == turnDir.right){
                RotateRight();
            }
            if(other.gameObject.GetComponent<RotateTrigger>().getTurnDir() == turnDir.left){
                RotateLeft();
            }
        }
        if(other.gameObject.tag == "Destructable")
        {
            Debug.Log("destroyed building");
            Destroy(other.gameObject);
        }
    }


    private void RotateLeft()
    {
        initRotate = true;
        moveDirection = moveDirection + new Vector3 (-1, 0, -1);
        rotationVec = rotationVec + new Vector3(0, -90, 0);
    }

    private void RotateRight()
    {
        initRotate = true;
        moveDirection = moveDirection + new Vector3 (1, 0, 1);
        rotationVec = rotationVec + new Vector3(0, 90, 0);
    }
}

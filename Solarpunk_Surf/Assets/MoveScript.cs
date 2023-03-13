using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScript : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * Time.deltaTime * 10;       
    }


    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("got hit");            
        }   
    }
}

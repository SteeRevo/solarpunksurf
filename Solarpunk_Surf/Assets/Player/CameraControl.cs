using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;

    public float camerax = 17;
    public float cameray;
    public float cameraz = -17;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  new Vector3(player.transform.position.x + camerax, 13 + cameray, player.transform.position.z + cameraz);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{



    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float lifetime = 12;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        time += Time.deltaTime;
        if (time > lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("collision!");
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("player!");
            //Debug.Log(damage);
            if(collider.gameObject.GetComponent<PlayerState>().isInvincible == false)
            {
                collider.gameObject.GetComponent<PlayerState>().Health -= damage;
                Destroy(gameObject);
                collider.gameObject.GetComponent<PlayerState>().InvinEnabled();
            }
                
        }
        if (collider.gameObject.tag == "Building")
        {
            Destroy(gameObject);
        }
        

    }
}


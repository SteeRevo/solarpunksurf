using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    

    

    [SerializeField]
    public int Damage { get; private set; }

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
}


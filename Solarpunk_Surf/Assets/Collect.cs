using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public delegate void CollectAction();
    public static event CollectAction OnCollect;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            OnCollect();
            Destroy(gameObject);
        }
    }
}

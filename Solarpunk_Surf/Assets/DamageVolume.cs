using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVolume : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;

    private void OnTriggerEnter(Collider collider)
    {
        //Debug.Log("collision!");
        if (collider.gameObject.tag == "Player")
        {
            //Debug.Log("player!");
            //Debug.Log(damage);
            if (collider.gameObject.GetComponent<PlayerState>().isInvincible == false)
            {
                collider.gameObject.GetComponent<PlayerState>().Health -= damage;
                collider.gameObject.GetComponent<PlayerState>().InvinEnabled();
            }
        }
    }
}

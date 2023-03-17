using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMines : MonoBehaviour
{
    private bool collideWPlayer = false;

    private int damage = 1;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip explodeClip;

    // the player has to set off the mines
    // the player has to within range of a mine to trigger the mine
    // once a mine is triggered, if there are any other mines in range they will be triggered
    // the mines explode at random times
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag("Player")) {
            //Debug.Log("collision!");
             Debug.Log("player!");
            //Debug.Log(damage);
            if(col.gameObject.GetComponent<PlayerState>().isInvincible == false)
            {
                col.gameObject.GetComponent<PlayerState>().Health -= damage;
                audioSource.PlayOneShot(explodeClip);
                Debug.Log(explodeClip.length);
                Destroy(gameObject, explodeClip.length);
                col.gameObject.GetComponent<PlayerState>().InvinEnabled();
                
            }
        }
        if (col.CompareTag("Mines")) {
            Debug.Log("yay");
        }
        if (collideWPlayer) {
            Destroy(gameObject, Random.Range(0.5f, 5.0f));
        }

    }

    // void OnTriggerEnter(Collider col) {
    //     if (col.tag == "Player") {
    //         triggerDestroyOtherMines = true;
    //         Debug.Log(triggerDestroyOtherMines);
    //     }

    //     if (triggerDestroyOtherMines) {
    //         GameObject tempPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
    //         tempPlane.name = "tempPlane";
    //         tempPlane.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    //         tempPlane.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
    //         // Destroy(gameObject, Random.Range(0.5f, 2.0f));

    //         if (col.name == "tempPlane") {
    //             Destroy(gameObject);
    //             Debug.Log("it collided yo");
    //         }
    //     }

        
    //     // if (col.gameObject.name == "CloneMine") {
    //     //     Debug.Log("checkkkkkk pls work for the love of god");
    //     // }
    //     // if (triggerDestroyMines && col.gameObject.tag == "CloneMine") {
    //     //         Debug.Log("destroy mines"+triggerDestroyMines);
    //     //         Destroy(gameObject, Random.Range(0.5f, 2.0f));
    //     // }
    // }
}

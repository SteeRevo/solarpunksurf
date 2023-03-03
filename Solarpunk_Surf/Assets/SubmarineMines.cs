using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMines : MonoBehaviour
{
    public GameObject originalMine;
    public GameObject mineContainer;

    void Start() {
        CreateMines(5);
    }
    void Update()
    {
        
    }

    void CreateMines(int mineNum) {
        for (int i = 0; i < mineNum; i++) {
            GameObject cloneMine = Instantiate(originalMine, new Vector3(originalMine.transform.position.x + (i * 2f), originalMine.transform.position.y, originalMine.transform.position.z + (i * 2f)), transform.rotation);
            cloneMine.name = "CloneMine-" + (i + 1);
            cloneMine.transform.parent = mineContainer.transform;
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            // Destory(instance.cloneMine);
        }
    }
}

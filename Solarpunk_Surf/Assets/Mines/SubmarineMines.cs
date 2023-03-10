using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMines : MonoBehaviour
{
    public GameObject originalMine;
    public GameObject mineContainer;

    void Start() {
        CreateMines(3);
    }

    // create the number of mines that you want
    void CreateMines(int mineNum) {
        for (int i = 0; i < mineNum; i++) {
            // figure out better placement for the cloned mines
            // they should be in random places, but still near each other
            // also change the radius of the boc collider which is the range of the mine
            GameObject cloneMine = Instantiate(originalMine, new Vector3(originalMine.transform.position.x + (i * 1.0f), originalMine.transform.position.y, originalMine.transform.position.z + (i * 0.5f)), transform.rotation);
            // cloneMine.name = "CloneMine-" + (i + 1);
            cloneMine.name = "CloneMine";
            Rigidbody mineRigidBody = cloneMine.AddComponent<Rigidbody>();
            mineRigidBody.useGravity = false;
            cloneMine.transform.parent = mineContainer.transform;
        }
    }
}

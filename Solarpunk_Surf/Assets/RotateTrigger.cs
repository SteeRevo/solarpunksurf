using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    [SerializeField]
    private turnDir turnWay = turnDir.right;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public turnDir getTurnDir()
    {
        return turnWay;
    }
}

public enum turnDir
{
    left, right
}

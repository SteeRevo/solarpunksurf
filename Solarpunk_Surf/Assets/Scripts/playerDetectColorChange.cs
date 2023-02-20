using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetectColorChange : MonoBehaviour
{

    public Transform player;
    public float whirlpoolRad;

    private float detectColor;
    private Renderer whirlpoolRenderer;

    void Start()
    {
        whirlpoolRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < whirlpoolRad) 
        {
            detectColor = 0f;
        }
        else
        {
            detectColor = 1f;
        }


        Color myColor = new Color(detectColor, detectColor, detectColor, 1);
        whirlpoolRenderer.material.color = myColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    
  
    [SerializeField]
    private Slider healthMeter;

    private int health;
    public int Health
    {
        get {return health;}

        set
        {
            health = value;
            Debug.Log(health);

            healthMeter.value = Mathf.InverseLerp(0, maxHealth, health) * 100;
            if (health <= 0)
            {
                Debug.Log("game over");
            }
        }
    }
    public int maxHealth = 20;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

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
            healthMeter.value = (health / maxHealth) * 100;
            Debug.Log(healthMeter.value);
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

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("collision!");
        // damaging objects must have tag 'DamageVolume' for now
        if (collider.gameObject.tag == "DamageVolume")
        {
            Debug.Log("collision!");
            // this only works with projectiles atm
            Health -= collider.GetComponent<Projectile>().Damage;
        }

    }

}

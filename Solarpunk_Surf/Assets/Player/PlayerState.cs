using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRender;
    
    private Color origColor;
  
    [SerializeField]
    private Slider healthMeter;

    [SerializeField]
    private GameObject gameOverScreen;

    public bool isInvincible = false;
    public bool gameOver = false;
    public float invinCoolddown = 3.0f;

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
                GameOver();
            }
        }
    }
    public int maxHealth = 20;

    public void InvinEnabled()
    {
        isInvincible = true;
        meshRender.material.color = Color.white;
        Debug.Log(meshRender.material.color);
        StartCoroutine(InvinDisable());
    }

    IEnumerator InvinDisable()
    {
        yield return new WaitForSeconds(invinCoolddown);
        isInvincible = false;
        meshRender.material.color = origColor;
        Debug.Log(meshRender.material.color);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        origColor = meshRender.material.color;
    }

    void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        GetComponent<PlayerController>().enabled = false;
        // how to enable menu controls?
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

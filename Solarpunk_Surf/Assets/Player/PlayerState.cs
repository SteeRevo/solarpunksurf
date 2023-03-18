using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        Time.timeScale = 1f;
    }

    void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
        GetComponent<PlayerController>().enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        // gets the 1th child object of the gameOverScreen which should be the retry button
        EventSystem.current.SetSelectedGameObject(gameOverScreen.transform.GetChild(1).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

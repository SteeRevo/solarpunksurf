using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controls : MonoBehaviour
{
    private InputManager playerActions;

    [SerializeField]
    private GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    // Start is called before the first frame update
    void Awake()
    {
        playerActions = new InputManager();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActions.UI.Pause.triggered){
            Debug.Log("Pause");
           PauseGame();
        }
    }


    private void PauseGame()
    {
        if(gameIsPaused){
            Resume();
        }
        else
        {
            Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }


    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.UI.Enable();
        // playerActions.UI.Enable();
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.UI.Disable();
        // player
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private InputManager playerActions;
    public static bool gameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;


    // Start is called before the first frame update
    void Start()
    {
        playerActions = new InputManager();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActions.UI.Pause.triggered){
            Debug.Log("Pause hit");
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

    //
    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }

    //
    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }



    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.UI.Enable();
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.UI.Disable();    
    }
}

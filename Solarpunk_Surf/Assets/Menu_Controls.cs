using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controls : MonoBehaviour
{
    private InputManager playerActions;

    [SerializeField]
    private GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    public delegate void PauseController();
    public static event PauseController OnPause;

    private bool inConvo = false;

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

    private IEnumerator afterPause()
    {
        yield return new WaitForEndOfFrame();
        OnPause();
    }


    private void PauseGame()
    {
        if(gameIsPaused)
        {
            Resume();
            
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        
        
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        OnPause();
    }

    public void Resume()
    {
        
        StartCoroutine(afterPause());
        if(!inConvo){
            Time.timeScale = 1f;
        }
        
        gameIsPaused = false;
        
        pauseMenuUI.SetActive(false);
        
    }

    private void checkPause()
    {
        inConvo = !inConvo;
    }


    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.UI.Enable();
        Dialogue.inDialogue += checkPause;
        // playerActions.UI.Enable();
        //Dialogue.inDialogue += checkDialogue;
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.UI.Disable();
        Dialogue.inDialogue -= checkPause;
        //Dialogue.inDialogue -= checkDialogue;
        // player
    }
}

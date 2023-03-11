using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    private InputManager playerActions;
    public static bool gameIsPaused = false;
    public GameObject resumeGameButton;
    public GameObject exitGameButton;

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject boostMeter;

    [SerializeField]
    private GameObject healthMeter;

    public GameObject playerScript;
    private PlayerController playerControllerScript;

    private bool _inDialogue = false;

    


    // Start is called before the first frame update
    void Start()
    {
        playerActions = new InputManager();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeGameButton);
        playerControllerScript = playerScript.GetComponent<PlayerController>();
        

        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActions.UI.Pause.triggered){
            
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

    //
    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeGameButton);
    }

    //
    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void checkDialogue()
    {
       Debug.Log("in dialogue");
    }



    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.UI.Enable();
        Dialogue.inDialogue += checkDialogue;
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.UI.Disable();    
        Dialogue.inDialogue -= checkDialogue;
    }
}

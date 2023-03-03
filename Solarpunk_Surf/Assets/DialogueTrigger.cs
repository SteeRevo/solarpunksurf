using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue Dialogue_script;
    public GameObject DialogueBox2;
    // public GameObject PlayerController;
    private InputManager playerActions;
    public PlayerController PlayerController_script;

    [SerializeField]
    private GameObject pauseMenuUI;

    public static bool gameIsPaused = false;
    // public PauseMenu PauseMenu_script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            Debug.Log("Collision detected");

            // PauseMenu_script.Pause();
            // playerActions.UI.Disable();
            Pause();
            PlayerController_script.OnDisable();

            DialogueBox2.SetActive(true);
            Dialogue_script.StartDialogue();
            // Dialogue_script.DialogueSystem();
        }
    }

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

    // private void OnTriggerExit(Collider col) {
    //     // PlayerController_script.OnEnable();
    //     DialogueBox2.SetActive(false);
    // }

}

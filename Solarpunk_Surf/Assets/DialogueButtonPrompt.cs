using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButtonPrompt : MonoBehaviour
{
    [SerializeField]
    private Dialogue dialogueBox;

    [SerializeField]
    private GameObject buttonPrompt;

    [SerializeField]
    private GameObject pauseMenuUI;

    private bool gameIsPaused = false;

    private bool inRange = false;

    private bool talkInput;

    private InputManager playerActions;

    
    // Start is called before the first frame update
    private void Awake()
    {
        playerActions = new InputManager();

        playerActions.Player.Talk.performed += _ => talkInput = true;
        playerActions.Player.Talk.canceled += _ => talkInput = false;
        buttonPrompt.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (talkInput)
        {
            Debug.Log("triggered dialogue");
            Pause();
            dialogueBox.gameObject.SetActive(true);
            dialogueBox.StartDialogue();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inRange = true;
            buttonPrompt.SetActive(true);
            Debug.Log(inRange);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            inRange = false;
            buttonPrompt.SetActive(false);
            Debug.Log(inRange);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }
}

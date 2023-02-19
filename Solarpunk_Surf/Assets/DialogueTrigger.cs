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
            PlayerController_script.OnDisable();
            DialogueBox2.SetActive(true);
            Dialogue_script.StartDialogue();
            // Dialogue_script.DialogueSystem();
        }
    }

    // private void OnTriggerExit(Collider col) {
    //     // PlayerController_script.OnEnable();
    //     DialogueBox2.SetActive(false);
    // }

}

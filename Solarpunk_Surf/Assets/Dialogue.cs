using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    // accessing json info
    // public TextAsset textJSON;

    // [System.Serializable]
    // public class DialogueClass {
    //     public int id;
    //     public string speaker;
    //     public string message;
    // }

    // [System.Serializable]
    // public class DialogueList {
    //     public DialogueClass[] DialogueLines;
    // }

    // public DialogueList myDialogueList = new DialogueList();
    // accessing json info above
    
    public TextMeshProUGUI textComponent;
    public string[] Lines; 
    public float textSpeed;

    private int index;

    public PlayerController PlayerController_script;

    void Awake() {
        Lines = new string[] {
            "Sweno, the Norways' king, craves composition:",
            "Nor would we deign him burial of his men",
            "Till he disbursed at Saint Colme's inch",
            "Ten thousand dollars to our general use."
        };

    }

    void Start() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        // myDialogueList = JsonUtility.FromJson<DialogueList>(textJSON.text);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == Lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = Lines[index];
            }
        }
    }

    public void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine() {

        textComponent.text = string.Empty;
        foreach(char c in Lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // this code is for accessing the json file
        // textComponent.text = string.Empty;
        // foreach(DialogueClass dialogue in myDialogueList.DialogueLines) {
        //     Debug.Log(dialogue.id);
        //     foreach(char c in dialogue.message.ToCharArray()) {
        //         textComponent.text += c;
        //         yield return new WaitForSeconds(textSpeed);
        //     }
        // }
    }

    void NextLine() {
        if (index < Lines.Length - 1) {
            index++;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        } 
        else {
            gameObject.SetActive(false);
            PlayerController_script.OnEnable();
        }
    }
}

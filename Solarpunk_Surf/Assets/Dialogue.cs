using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class DialogueClass {
        public string speaker;
        public string message;
    }

    [System.Serializable]
    public class DialogueList {
        public DialogueClass[] DialogueLines;
    }

    public DialogueList myDialogueList = new DialogueList();

    //
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int index;

    public PlayerController PlayerController_script;

    // Start is called before the first frame update
    void Start()
    {
        myDialogueList = JsonUtility.FromJson<DialogueList>(textJSON.text);
        textComponent.text = "";
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // NextLine();
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    public void DialogueSystem() {
        StartDialogue();
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine() {
        foreach(char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine() {
        // Debug.Log(lines.Length);
        if (index < lines.Length - 1) {
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

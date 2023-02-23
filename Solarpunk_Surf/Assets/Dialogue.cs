using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    public Sprite speakerImage;
    public TextMeshProUGUI speakerTextComponent;

    public TextMeshProUGUI textComponent;
    public List<string> Lines; 
    public float textSpeed;

    private int index;

    public PlayerController PlayerController_script;

    public JSONReader JSONReader_script;

    void Start() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        speakerTextComponent.text = string.Empty;
        accessJSONDialogue();
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
        speakerTextComponent.text = JSONReader_script.currentSpeaker[index];
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine() {

        textComponent.text = string.Empty;
        foreach(char c in Lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    void NextLine() {
        if (index < Lines.Count-1) {
            index++;
            textComponent.text = "";
            speakerTextComponent.text = JSONReader_script.currentSpeaker[index];
            StartCoroutine(TypeLine());
        } 
        else {
            gameObject.SetActive(false);
            PlayerController_script.OnEnable();
        }
    }

    void accessJSONDialogue() {
        JSONReader_script.readJSONFile();
        Lines = JSONReader_script.MessageLines;

        // speakerTextComponent.text = JSONReader_script.currentSpeaker;
        
        // Lines = JSONReader_script.MessageLines;
    }

}

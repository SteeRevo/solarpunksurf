using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JSONReader : MonoBehaviour
{

    public TextAsset textJSON;

    [System.Serializable]
    public class DialogueClass {
        public string speaker;
        public string message;
        public string speakerimage;

    }

    [System.Serializable]
    public class DialogueList {
        public DialogueClass[] DialogueLines;
    }

    public DialogueList myDialogueList = new DialogueList();

    public  List<string> MessageLines = new List<string>();  

    public List<string> currentSpeaker = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        myDialogueList = JsonUtility.FromJson<DialogueList>(textJSON.text);
        foreach(DialogueClass dialogue in myDialogueList.DialogueLines) {
           currentSpeaker.Add(dialogue.speaker);
            MessageLines.Add(dialogue.message);
            // Debug.Log(dialogue.message);
        }
    }

    void Update() {
        
    }

    // public void readJSONFile() {
    //     Debug.Log("inside");
    //     foreach(DialogueClass dialogue in myDialogueList.DialogueLines) {
    //     //    currentSpeaker.Add(dialogue.speaker);
    //     //     MessageLines.Add(dialogue.message);
    //         Debug.Log(dialogue.message);
    //     }
    // }

    public List<string> getDialogueMessageList() {
        return MessageLines;
    }

     public List<string> getCurrentSpeakerList() {
        return currentSpeaker;
    }
}

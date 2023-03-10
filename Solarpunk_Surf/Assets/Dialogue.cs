using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Dialogue : MonoBehaviour
{
    // public Sprite speakerImage;
    public TextMeshProUGUI speakerTextComponent;

    public TextMeshProUGUI textComponent;
    public List<string> Lines = new List<string>(); 
    public List<string> currentSpeakerList = new List<string>(); 
    // public string[] Lines;
    public float textSpeed;

    private int dialogueIndex;

    public PlayerController PlayerController_script;

    private InputManager playerActions;

    [SerializeField]
    private GameObject pauseMenuUI;

    public static bool gameIsPaused = false;

    // public PauseMenu PauseMenu_script;

    public JSONReader JSONReader_script;

    // public string[] messageDialogueLines;

    public delegate void CutsceneEvent();
    public static event CutsceneEvent inDialogue;
    private bool inPause = false;

    public void Awake() {
        // foreach (string i in JSONReader_script.MessageLines) {
        //     Lines.Add(i);
        // }

        // Lines = new string[] {
        //     "Sweno, the Norways' king, craves composition:",
        //     "Nor would we deign him burial of his men",
        //     "Till he disbursed at Saint Colme's inch",
        //     "Ten thousand dollars to our general use."
        // };

        // Lines = new List<string> ( new string[JSONReader_script.MessageLines.Count] );
        // accessJSONDialogue();
        
        // foreach (string i in JSONReader_script.MessageLines) {
        //     Lines.Add(i);
        //     Debug.Log("it is work"+i);
        // }

        playerActions = new InputManager();
    }
    void Start() {
        gameObject.SetActive(false);
        textComponent.text = string.Empty;
        speakerTextComponent.text = string.Empty;
        // accessJSONDialogue();
        // foreach (string i in JSONReader_script.MessageLines) {
        //     Lines.Add(i);
        // }
        Lines = JSONReader_script.getDialogueMessageList();
        currentSpeakerList = JSONReader_script.getCurrentSpeakerList();
        // foreach (string i in JSONReader_script.MessageLines) {
        //     Lines.Add(i);
        //     Debug.Log("it is work"+i);
        // }
        
    }

    // Update is called once per frame
    void Update() {
        if(!inPause){
            if (playerActions.UI.Select.triggered) {
                if (textComponent.text == Lines[dialogueIndex]) {
                    NextLine();
                }
                else {
                    StopAllCoroutines();
                    textComponent.text = Lines[dialogueIndex];
                }
            }
        }
        
    }

    public void StartDialogue() {
        dialogueIndex = 0;
        speakerTextComponent.text = currentSpeakerList[dialogueIndex];
        inDialogue();
        StartCoroutine(TypeLine());
    }

    public IEnumerator TypeLine() {

        textComponent.text = string.Empty;
        foreach(char c in Lines[dialogueIndex].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }

    }

    void NextLine() {
        if (dialogueIndex < Lines.Count-1) {
            dialogueIndex++;
            textComponent.text = "";
             speakerTextComponent.text = currentSpeakerList[dialogueIndex];
            StartCoroutine(TypeLine());
        } 
        else {
            inDialogue();
            gameObject.SetActive(false);
            Resume();
            // PauseMenu_script.Resume();
            // playerActions.UI.Enable();
            // PlayerController_script.OnEnable();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
        //pauseMenuUI.SetActive(true);
    }

    //
    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        //pauseMenuUI.SetActive(false);
        
    }

    private void changePause()
    {
        inPause = !inPause;
    }

    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.Player.Enable();
        playerActions.UI.Enable();
        Menu_Controls.OnPause += changePause;
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.Player.Disable();
        playerActions.UI.Disable();    
        Menu_Controls.OnPause -= changePause;
    }

}

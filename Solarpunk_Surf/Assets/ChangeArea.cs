using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeArea : MonoBehaviour
{
    public int levelIndex;

    public GameObject Player;

    public Animator transitionAnim;

    public GameObject rankObject;

    public TMP_Text rankText;

    public TMP_Text timeText;   

    public TMP_Text healthText;

    public TMP_Text currentTimer;
    
    private PlayerState playerSt;

    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = Player.GetComponent<PlayerController>();
        playerSt     = Player.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player" && playerScript.checkItem())
        {
            rankObject.SetActive(true);
            float timeCount = float.Parse(currentTimer.text);
            Debug.Log(timeCount);
            timeText.text = "Time: " + currentTimer.text;
            healthText.text = "Health: " + playerSt.Health.ToString();
            if(timeCount < 15 && playerSt.Health > 4)
            {
                rankText.text = "Rank: S";
            }
            else if (timeCount < 20 && playerSt.Health > 3)
            {
                rankText.text = "Rank: A";
            }
            else if (timeCount < 25 && playerSt.Health > 2)
            {
                rankText.text = "Rank: B";
            }
            else if (timeCount < 30 && playerSt.Health > 1)
            {
                rankText.text = "Rank: C";
            }
            else {
                rankText.text = "Rank: D";
            }
            playerScript.OnDisable();
            StartCoroutine(LoadScene());
             
        }
    }

    IEnumerator LoadScene()
    {
        
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(7.1f);
        SceneManager.LoadScene(levelIndex);
    }
   
}

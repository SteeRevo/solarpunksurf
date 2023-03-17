using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChangeAreaNormal : MonoBehaviour
{
    public int levelIndex;

    public PlayerController playerScript;

    public Animator transitionAnim;

    public GameObject rankObject;

    public TMP_Text rankText;

    public TMP_Text timeText;   

    public TMP_Text healthText;

    public TMP_Text currentTimer;
    
    public PlayerState playerSt;

    public GameObject turrets;

    [SerializeField]
    int SRankTime, SRankHealth, ARankTime, ARankHealth, 
    BRankTime, BRankHealth, CRankTime, CRankHealth; 

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Player")
        {
            turrets.SetActive(false);
            rankObject.SetActive(true);
            float timeCount = float.Parse(currentTimer.text);
            Debug.Log(timeCount);
            timeText.text = "Time: " + currentTimer.text;
            healthText.text = "Health: " + playerSt.Health.ToString();
            if(timeCount < SRankTime && playerSt.Health > SRankHealth)
            {
                rankText.text = "Rank: S";
            }
            else if (timeCount < ARankTime && playerSt.Health > ARankHealth)
            {
                rankText.text = "Rank: A";
            }
            else if (timeCount < BRankTime && playerSt.Health > BRankHealth)
            {
                rankText.text = "Rank: B";
            }
            else if (timeCount < CRankTime && playerSt.Health > CRankHealth)
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
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene(levelIndex);
    }
}

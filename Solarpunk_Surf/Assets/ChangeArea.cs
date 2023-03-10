using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeArea : MonoBehaviour
{
    public int levelIndex;

    public GameObject Player;

    public Animator transitionAnim;

    private PlayerController playerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = Player.GetComponent<PlayerController>();
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
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(2.1f);
        SceneManager.LoadScene(levelIndex);
    }
   
}

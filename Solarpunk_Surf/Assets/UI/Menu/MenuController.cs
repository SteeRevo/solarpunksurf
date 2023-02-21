using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{

    public string _newGameLevel;
    private string levelToLoad;
    public GameObject newGameButton;
    public GameObject exitGameButton;

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

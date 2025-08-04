using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    VisualElement root;
    VisualElement container;
    PlayerControls playerLink;
    [SerializeField] VisualTreeAsset Icon;
    VisualElement menu;
    bool checkCLicks;


    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        container = root.Query<VisualElement>("Container");
        playerLink = FindObjectOfType<PlayerControls>();
       
        menu = root.Query<VisualElement>("Menu");
        menu.visible = true;
    }


    void Update()
    {
        if(checkCLicks)
        {
            root.RegisterCallback<ClickEvent>(OnButtonClick);
        }
    }

       public void GameStart()
    {
        menu.visible = true;
        checkCLicks = true;
    }

    void OnButtonClick (ClickEvent eventInfo)
    {
        var targetString = eventInfo.target.ToString();

        if(targetString.Contains("REPLAY"))
        {
            SceneManager.LoadScene("Game");
        }

        else if (targetString.Contains("QUIT"))
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }
        else if (targetString.Contains("Setting"))
        {
           
           
        }

    }

}

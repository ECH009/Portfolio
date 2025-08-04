using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
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
        SetUpIcons();
        menu = root.Query<VisualElement>("Menu");
        menu.visible = false;
    }


    void Update()
    {
        if(checkCLicks)
        {
            root.RegisterCallback<ClickEvent>(OnButtonClick);
        }
    }

    public void AddIcon()
    {
        container.Add(Icon.Instantiate());

    }
    public void RemoveIcon()
    {
        container.RemoveAt(0);

    }

    private void SetUpIcons()
    {
        for (int count = (int)playerLink.playerLives; count > 0; count--)
            { 
               AddIcon();
            }
    }

    public void GameOver()
    {
        menu.visible = true;
        checkCLicks = true;
    }

    void OnButtonClick (ClickEvent eventInfo)
    {
        var targetString = eventInfo.target.ToString();

        if(targetString.Contains("REPLAY"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else if (targetString.Contains("QUIT"))
        {
            Application.Quit();
            Debug.Log("Quit Game");
        }

    }

}

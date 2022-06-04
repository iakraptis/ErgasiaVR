using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    public Button startButton;
    public Button controlButton;
    public Button exitButton;
    public Label controlText;
    private bool controlsVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        // initialize buttons
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("StartButton");
        controlButton = root.Q<Button>("ControlButton");
        exitButton = root.Q<Button>("ExitButton");
        controlText = root.Q<Label>("ControlText");

       // call button functions
        exitButton.RegisterCallback<ClickEvent>(ev => ExitPressed());
        startButton.RegisterCallback<ClickEvent>(ev => StartPressed());
        controlButton.RegisterCallback<ClickEvent>(ev => ControlPressed());
        controlText.style.display = DisplayStyle.None;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitPressed();
        }
    }

    void ExitPressed()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }

    void StartPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    
    
    void ControlPressed()
    {

        // enable control label, disable other buttons
        if (controlsVisible == false)
        {
            controlsVisible = true;
            controlText.style.display = DisplayStyle.Flex;
            startButton.style.display = DisplayStyle.None;
            controlButton.style.display = DisplayStyle.Flex;
            exitButton.style.display = DisplayStyle.None;
            controlButton.text = "Back";

        }
        else
        {
            controlsVisible = false;
            controlText.style.display = DisplayStyle.None;
            startButton.style.display = DisplayStyle.Flex;
            controlButton.style.display = DisplayStyle.Flex;
            controlButton.text = "Controls";
            exitButton.style.display = DisplayStyle.Flex;


        }


    }
}
;
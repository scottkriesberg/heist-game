using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hacker : MonoBehaviour
{
    // public DoorController doorCtrl;

    enum Screen {MainMenu, Password, Win};
	Screen currentScreen = Screen.MainMenu;
    string password = "12345";

    Text commandText;
    string CommandReference;

    // Start is called before the first frame update
    void Start()
    {
        Greetings();
        // doorCtrl = GameObject.Find("CardSensor").GetComponent<DoorController>();
        commandText = GameObject.Find("CommandList/Viewport/Content/Text").GetComponent<Text>();
        
        TextAsset txtAssets = (TextAsset)Resources.Load("CommandReference");
        CommandReference = txtAssets.text;
        commandText.text = CommandReference;
    }

    void Greetings() {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
		Terminal.WriteLine("Enter your commands:");
    }

    void OnUserInput(string input) {
        input = input.Substring(17);
        if (input == "exit") {
		    Greetings();
		} 
        else if (currentScreen == Screen.MainMenu) {
		    MainMenuScreen(input);
		} 
        else if (currentScreen == Screen.Password) {
		    CheckPassword(input);
		}   
	}

    void CheckPassword(string input) {
        if (input == password) {
		    Terminal.WriteLine("Door unlocked");
            currentScreen = Screen.MainMenu;
		} 
        else {
		    Terminal.WriteLine("WRONG PASSWORD!");
            Terminal.WriteLine("Try again or type exit to go back");
		}
    }

    void MainMenuScreen(string input) {
        switch(input) {
		    case "clear":
		        Terminal.ClearScreen();
		        break;
                /*
            case "open the door":
                doorCtrl.hackIntotheDoor();
		        break;
                */
            case "open door #1":
                Terminal.WriteLine("Enter the password:");
                currentScreen = Screen.Password;
		        break;
            case "open test.txt":
                OpenFile("test");
                break;
            case "return":
                commandText.text = CommandReference;
                break;
		    default:
		        Terminal.WriteLine("Invalid command");
		        break;
		}
    }

    void OpenFile(string name) {
        TextAsset txtAssets = (TextAsset)Resources.Load(name);
        string txtContents = txtAssets.text;
        commandText.text = txtContents;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

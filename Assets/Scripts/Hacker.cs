using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hacker : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cellButtons;

    enum Screen {MainMenu, Password, Win};
	Screen currentScreen = Screen.MainMenu;
    string password = "A17";

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
		    Terminal.WriteLine("Success!");
            currentScreen = Screen.MainMenu;
            this.OnPasswordAccept();
		} 
        else {
		    Terminal.WriteLine("Wrong password!");
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
            case "ls":
		        Terminal.WriteLine("lasers.txt    lock.txt    guard.txt");
                Terminal.WriteLine("camera.txt    cell.txt    buttons.txt");
		        break;
            case "enable_buttons":
                Terminal.WriteLine("Enter the password:");
                currentScreen = Screen.Password;
		        break;
            case "open cell.txt":
                OpenFile("cell");
                break;
            case "open buttons.txt":
                OpenFile("buttons");
                break;
            case "open camera.txt":
                OpenFile("camera");
                break;
            case "open guard.txt":
                OpenFile("guard");
                break;
            case "open laser.txt":
                OpenFile("laser");
                break;
            case "open lock.txt":
                OpenFile("lock");
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

    void OnPasswordAccept()
    {
        foreach (GameObject g in this.cellButtons)
        {
            g.SetActive(true);
        }
    }
}

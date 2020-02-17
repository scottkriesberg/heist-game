using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Greetings();
    }

    void Greetings() {
		Terminal.WriteLine("Enter your commands:");
    }

    void OnUserInput(string input){
        input = input.Substring(17);
        switch(input) {
		    case "1": 
		        Terminal.WriteLine("Pressed 1");
		        break;
		    case "clear":
		        Terminal.ClearScreen();
		        break;
		    default:
		        Terminal.WriteLine("Invalid command");
		        break;
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}

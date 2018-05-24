using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // game state
    // use member variables to hold state
    int level;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Use this for initialization
	void Start () {
        ShowMainMenu("Hello Sir!");
	}

    // method for showing main menu of game
    void ShowMainMenu(string greeting) {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();

        if (greeting != null) {
            Terminal.WriteLine(greeting);
        }

        Terminal.WriteLine("Welcome to Terminal Hacker!");
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for a magnifying glass");
        Terminal.WriteLine("Press 2 for the Planetarium");
        Terminal.WriteLine("Press 3 for the Hubbell Space Telescope");
        Terminal.WriteLine("Enter your selection:");
    }

    // message (special function) called when the user hits return
    void OnUserInput (string input) 
    {
        
        if (input == "menu") // we can always go directly to main menu
        { 
            ShowMainMenu(null); 
        } 
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    private void CheckPassword(string input)
    {
        throw new NotImplementedException();
    }

    private void RunMainMenu(string input)
    {
        string[] validLevels = { "1", "2", "3" }; // these are the valid levels that can be selected
        if (Array.IndexOf(validLevels, input) > -1)
        {
            level = Int32.Parse(input);
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please choose a valid option.");
        }
    }

    // code to handle password loop of game
    void StartGame() 
    {
        currentScreen = Screen.Password;
        Terminal.WriteLine("You have chosen level: " + level);
        Terminal.WriteLine("Please enter your password:");
    }
}

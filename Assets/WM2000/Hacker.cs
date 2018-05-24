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
    string targetPassword;

    // Use this for initialization
	private void Start () {
        ShowMainMenu("Hello Sir!");
	}

    // method for showing main menu of game
    private void ShowMainMenu(string greeting) {
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
    private void OnUserInput (string input) 
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
    private void StartGame() 
    {
        currentScreen = Screen.Password;
        GenerateTargetPassword();
        Terminal.WriteLine("You have chosen level: " + level);
        Terminal.WriteLine("Please enter your password:");
    }

    private void GenerateTargetPassword()
    {
        // create password dictionary
        Dictionary<int, string[]> passwords = new Dictionary<int, string[]>();
        passwords.Add(1, new string[] { "Dirt", "Bug", "See", "Eye", "Key" });
        passwords.Add(2, new string[] { "Earth", "Clear", "Light", "Secret", "Stars" });
        passwords.Add(3, new string[] { "Observation", "Telescope", "Conversation", "Nighttime", "Forensics" });

        // select correct password list based on level
        string[] currentPasswordList = passwords[level];

        // get random password from list and set game state
        System.Random randomIndex = new System.Random();
        targetPassword = currentPasswordList[randomIndex.Next(currentPasswordList.Length)];

    }

    private void CheckPassword(string input)
    {
        if (input.ToLower() == targetPassword.ToLower())
        {
            RunWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect password. Please try again.");
        }
    }

    private void RunWinScreen()
    {
        currentScreen = Screen.Win;
        Terminal.WriteLine("");
        Terminal.WriteLine("You have hacked into the system.");
        Terminal.WriteLine("Please type 'menu' to play again.");
    }
}

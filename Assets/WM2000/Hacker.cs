using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game Configuration Data
    string[] level1passwords = { "Dirt", "Bug", "See", "Eye", "Key" };
    string[] level2passwords = { "Earth", "Clear", "Light", "Secret", "Stars" };
    string[] level3passwords = { "Observation", "Telescope", "Conversation", "Nighttime", "Forensics" };

    // Game state
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
            level = int.Parse(input);
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
        Terminal.ClearScreen();
        Terminal.WriteLine("Scrambled password: " + JumbleString(targetPassword));
        Terminal.WriteLine("Please enter your password:");
    }

    private string JumbleString(string targetPassword)
    {
        // initialize variables
        string jumbledPassword = "";
        char[] passwordCharacters = targetPassword.ToLower().ToCharArray();

        // pluck random character and add to jumbled password
        while(passwordCharacters.Length > 0) 
        {
            int randomIndex = UnityEngine.Random.Range(0, passwordCharacters.Length);
            char thisChar = passwordCharacters[randomIndex];
            jumbledPassword += thisChar;

            // substitute last character in array for what we just plucked, and decrement array size by one
            passwordCharacters[randomIndex] = passwordCharacters[passwordCharacters.Length - 1];
            Array.Resize(ref passwordCharacters, passwordCharacters.Length - 1);
        }

        return jumbledPassword;
    }

    private void GenerateTargetPassword()
    {
        // create password dictionary
        Dictionary<int, string[]> passwords = new Dictionary<int, string[]>();
        passwords.Add(1, level1passwords);
        passwords.Add(2, level2passwords);
        passwords.Add(3, level3passwords);
        if (level < 1 || level > 3 ) { Debug.LogError("Invalid level number"); } // raise error for invalid level number

        // select correct password list based on level
        string[] currentPasswordList = passwords[level];

        // get random password from list and set game state
        int randomIndex = UnityEngine.Random.Range(0, currentPasswordList.Length);
        targetPassword = currentPasswordList[randomIndex];
    }

    private void CheckPassword(string input)
    {
        if (input.ToLower() == targetPassword.ToLower()) // we don't care about uppercase or lowercase for this game
        {
            RunWinScreen();
        }
        else
        {
            Terminal.WriteLine("Incorrect password. Please try again.");
            Terminal.WriteLine("Scrambled password: " + JumbleString(targetPassword));
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

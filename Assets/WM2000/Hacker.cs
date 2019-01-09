using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {

    // Game config data
    readonly string[] level1Passwords = { "hello", "world" };
    readonly string[] level2Passwords = { "goodbye", "universe" };

    // Game state
    int level;
    string answer; 
    int failCount = 0;
    int allowedFailedCount;
    // Screens
    enum Screen { MainMenu, Password, Success, Failure };
    Screen currentScreen = Screen.MainMenu;

    // Use this for initialization
    void Start () {
        InitMainMenu();
        print(level1Passwords.Length);
    }

    // Update is called once per frame
    void Update() {

    }

    // Initializes the main menu
    void InitMainMenu () {
        currentScreen = Screen.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("Oh sup doggie");
        Terminal.WriteLine("Long time no se... oh wait, you're not the normal doggie");
        Terminal.WriteLine("You gotta prove yourself home doggie I donnu if I can trust ya k?");
    }


    // Hanldes the user input
    void OnUserInput (string input) {
        if (input == "menu" || currentScreen == Screen.Failure || currentScreen == Screen.Success) {
            InitMainMenu();
        } else if (currentScreen == Screen.MainMenu) {
            RunMainMenu(input);
        } else if (currentScreen == Screen.Password) {
            RunPasswordScreen(input);
        }
    }

    // Handles inputs relevent to the menu screen
    void RunMainMenu (string input) {
        bool isValidLevel = (input == "1" || input == "2");
        if (isValidLevel) {
            level = int.Parse(input);
            StartGame();
        } else {
            Terminal.WriteLine("Please choose a valid level.");
        }
    }

    // Handles the evaluation of correct answers and checks fail counts.
    // If the password was successful, show the Success screen.
    // If they fail, show for Failure screen.
    void RunPasswordScreen (string input) {
        if (input == answer) {
            RunSuccessScreen();
        } else if (failCount + 1 == allowedFailedCount) {
            FailGame();
        } else {
            failCount = failCount + 1;
            Terminal.WriteLine("You've failed " + failCount + " times. You have " + (allowedFailedCount - failCount) + " tries remaining, make them count.");
        }
    }

    // Runs the success screen
    void RunSuccessScreen() {
        currentScreen = Screen.Success;
        Terminal.ClearScreen();
        Terminal.WriteLine("Nailed it! " + answer + " was the correct answer.");
        Terminal.WriteLine("Sorry I doubted you home doggie");
        Terminal.WriteLine("Press any key to logout");
    }

    // Starts the game. Sets the desired answer based on the chosen level.
    void StartGame() {
        InitLevel();
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
    }

    // Evaluates what level was chosen and selects the appropriate password and fail count based on their selection
    void InitLevel() {
        switch (level)
        {
            case 1:
                answer = level1Passwords[Random.Range(0, level1Passwords.Length)];
                allowedFailedCount = 3;
                break;
            case 2:
                answer = level2Passwords[Random.Range(0, level2Passwords.Length)];
                allowedFailedCount = 2;
                break;
            default:
                Debug.LogError("Please choose a valid level");
                break;
        }
    }


    // Ends the game in a failure state.
    void FailGame() {
        currentScreen = Screen.Failure;
        Terminal.ClearScreen();
        Terminal.WriteLine("Maximum tries succeeded. Nice try, buddy.");
        Terminal.WriteLine("Enter any key to return to the Menu Screen.");
    }

}

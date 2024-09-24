using UnityEngine;
using TMPro;  // Needed for TextMeshPro
using UnityEngine.SceneManagement;

public class LearningMode : MonoBehaviour
{
    public TextMeshProUGUI stateText;  // UI Text for displaying the state name
    public TextMeshProUGUI capitalText;  // UI Text for displaying the capital name
    private int currentIndex = 0;

    // Array holding all states and their capitals
    private string[,] statesAndCapitals = new string[,]
    {
        {"Alabama", "Montgomery"},
        {"Alaska", "Juneau"},
        {"Arizona", "Phoenix"},
        {"Arkansas", "Little Rock"},
        {"California", "Sacramento"},
        {"Colorado", "Denver"},
        {"Connecticut", "Hartford"},
        {"Delaware", "Dover"},
        {"Florida", "Tallahassee"},
        {"Georgia", "Atlanta"},
        {"Hawaii", "Honolulu"},
        {"Idaho", "Boise"},
        {"Illinois", "Springfield"},
        {"Indiana", "Indianapolis"},
        {"Iowa", "Des Moines"},
        {"Kansas", "Topeka"},
        {"Kentucky", "Frankfort"},
        {"Louisiana", "Baton Rouge"},
        {"Maine", "Augusta"},
        {"Maryland", "Annapolis"},
        {"Massachusetts", "Boston"},
        {"Michigan", "Lansing"},
        {"Minnesota", "Saint Paul"},
        {"Mississippi", "Jackson"},
        {"Missouri", "Jefferson City"},
        {"Montana", "Helena"},
        {"Nebraska", "Lincoln"},
        {"Nevada", "Carson City"},
        {"New Hampshire", "Concord"},
        {"New Jersey", "Trenton"},
        {"New Mexico", "Santa Fe"},
        {"New York", "Albany"},
        {"North Carolina", "Raleigh"},
        {"North Dakota", "Bismarck"},
        {"Ohio", "Columbus"},
        {"Oklahoma", "Oklahoma City"},
        {"Oregon", "Salem"},
        {"Pennsylvania", "Harrisburg"},
        {"Rhode Island", "Providence"},
        {"South Carolina", "Columbia"},
        {"South Dakota", "Pierre"},
        {"Tennessee", "Nashville"},
        {"Texas", "Austin"},
        {"Utah", "Salt Lake City"},
        {"Vermont", "Montpelier"},
        {"Virginia", "Richmond"},
        {"Washington", "Olympia"},
        {"West Virginia", "Charleston"},
        {"Wisconsin", "Madison"},
        {"Wyoming", "Cheyenne"}
    };

    void Start()
    {
        // Display the first state and capital when the game starts
        DisplayCurrentStateAndCapital();
    }

    // Method to display the current state and capital
    private void DisplayCurrentStateAndCapital()
    {
        if (stateText != null && capitalText != null && statesAndCapitals.Length > 0)
        {
            // Update the UI Text components with the state and capital
            stateText.text = "State: " + statesAndCapitals[currentIndex, 0];
            capitalText.text = "Capital: " + statesAndCapitals[currentIndex, 1];
        }
        else
        {
            Debug.LogError("StateText, CapitalText, or StatesAndCapitals not properly set.");
        }
    }

    // Method to go to the next state and capital
    public void NextState()
    {
        currentIndex++;
        if (currentIndex >= statesAndCapitals.GetLength(0))
        {
            currentIndex = 0;  // Loop back to the first state if at the end
        }
        DisplayCurrentStateAndCapital();
    }

    // Method to return to the main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Replace with your main menu scene name
    }
}

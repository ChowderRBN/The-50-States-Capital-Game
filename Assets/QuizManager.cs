using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI stateText;
    public TMP_InputField answerInputField;
    public Button submitButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI strikesText;
    public GameObject gameOverPanel;

    private Dictionary<string, string> stateCapitals;
    private List<string> availableStates;
    private string correctAnswer;
    private int score = 0;
    private int strikes = 0;
    private int maxStrikes = 3;
    private int autoCorrectThreshold = 2; // Maximum distance for auto-correct to apply

    void Start()
    {
        // Initialize state capitals data
        stateCapitals = new Dictionary<string, string>
        {
            { "Alabama", "Montgomery" }, { "Alaska", "Juneau" }, { "Arizona", "Phoenix" },
            { "Arkansas", "Little Rock" }, { "California", "Sacramento" }, { "Colorado", "Denver" },
            { "Connecticut", "Hartford" }, { "Delaware", "Dover" }, { "Florida", "Tallahassee" },
            { "Georgia", "Atlanta" }, { "Hawaii", "Honolulu" }, { "Idaho", "Boise" },
            { "Illinois", "Springfield" }, { "Indiana", "Indianapolis" }, { "Iowa", "Des Moines" },
            { "Kansas", "Topeka" }, { "Kentucky", "Frankfort" }, { "Louisiana", "Baton Rouge" },
            { "Maine", "Augusta" }, { "Maryland", "Annapolis" }, { "Massachusetts", "Boston" },
            { "Michigan", "Lansing" }, { "Minnesota", "St. Paul" }, { "Mississippi", "Jackson" },
            { "Missouri", "Jefferson City" }, { "Montana", "Helena" }, { "Nebraska", "Lincoln" },
            { "Nevada", "Carson City" }, { "New Hampshire", "Concord" }, { "New Jersey", "Trenton" },
            { "New Mexico", "Santa Fe" }, { "New York", "Albany" }, { "North Carolina", "Raleigh" },
            { "North Dakota", "Bismarck" }, { "Ohio", "Columbus" }, { "Oklahoma", "Oklahoma City" },
            { "Oregon", "Salem" }, { "Pennsylvania", "Harrisburg" }, { "Rhode Island", "Providence" },
            { "South Carolina", "Columbia" }, { "South Dakota", "Pierre" }, { "Tennessee", "Nashville" },
            { "Texas", "Austin" }, { "Utah", "Salt Lake City" }, { "Vermont", "Montpelier" },
            { "Virginia", "Richmond" }, { "Washington", "Olympia" }, { "West Virginia", "Charleston" },
            { "Wisconsin", "Madison" }, { "Wyoming", "Cheyenne" }
        };

        // Initialize the list of available states
        availableStates = new List<string>(stateCapitals.Keys);

        // Start the game by setting a new question
        gameOverPanel.SetActive(false);
        SetNewQuestion();
        UpdateUI();

        // Set the Submit Button's listener
        submitButton.onClick.AddListener(OnSubmitAnswer);
    }

    void SetNewQuestion()
    {
        if (availableStates.Count == 0)
        {
            GameOver();
            return;
        }

        // Select a random state from the available states
        string randomState = availableStates[Random.Range(0, availableStates.Count)];
        correctAnswer = stateCapitals[randomState];

        // Remove the state from the list to avoid repetition
        availableStates.Remove(randomState);

        // Update the state text
        stateText.text = randomState;

        // Clear the input field
        answerInputField.text = "";
    }

    void OnSubmitAnswer()
    {
        string playerAnswer = answerInputField.text.Trim();

        // Check if the answer is correct, allowing for typos using the Levenshtein Distance
        if (IsCorrectAnswer(playerAnswer, correctAnswer))
        {
            score++;
        }
        else
        {
            strikes++;
            if (strikes >= maxStrikes)
            {
                GameOver();
                return;
            }
        }

        UpdateUI();
        SetNewQuestion();
    }

    bool IsCorrectAnswer(string playerAnswer, string correctAnswer)
    {
        // Check if the answers are exactly the same
        if (string.Equals(playerAnswer, correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        // Check if the answers are similar enough (within the auto-correct threshold)
        int distance = LevenshteinDistance(playerAnswer.ToLower(), correctAnswer.ToLower());
        return distance <= autoCorrectThreshold;
    }

    // Levenshtein Distance algorithm implementation
    int LevenshteinDistance(string s1, string s2)
    {
        int[,] distances = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
            distances[i, 0] = i;

        for (int j = 0; j <= s2.Length; j++)
            distances[0, j] = j;

        for (int i = 1; i <= s1.Length; i++)
        {
            for (int j = 1; j <= s2.Length; j++)
            {
                int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;
                distances[i, j] = Mathf.Min(
                    Mathf.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                    distances[i - 1, j - 1] + cost
                );
            }
        }

        return distances[s1.Length, s2.Length];
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        strikesText.text = "Strikes: " + strikes + "/" + maxStrikes;
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        answerInputField.interactable = false;
        submitButton.interactable = false;
    }

    public void RestartGame()
    {
        // Reset game state
        score = 0;
        strikes = 0;

        // Reset the available states list
        availableStates = new List<string>(stateCapitals.Keys);

        gameOverPanel.SetActive(false);
        answerInputField.interactable = true;
        submitButton.interactable = true;

        SetNewQuestion();
        UpdateUI();
    }
}

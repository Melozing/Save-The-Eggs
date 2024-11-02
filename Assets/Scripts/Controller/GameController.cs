using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField] private Text _scoreText; // Text UI to display current score
    [SerializeField] private Text _scoreTextResult; // Text UI to display current score
    [SerializeField] private Text _highestScoreText; // Text UI to display highest score
    [SerializeField] private GameObject _gameOverPanel; // Panel to show when game is over
    [SerializeField] private GameObject _tapToPlayPanel; // Panel for starting the game
    [SerializeField] private SpawnItems _spawnItems; // Reference to the item spawning script

    private int _score; // Current score of the player
    private int _highestScore; // Highest score achieved by the player
    private bool _isGameOver; // Flag to check if the game is over

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set singleton instance
            //DontDestroyOnLoad(gameObject); // Keep this object across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    private void Start()
    {
        _scoreText.gameObject.SetActive(false); // Hide score text initially
        ShowTapToPlay(); // Show the tap to play panel
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != this)
        {
            Destroy(gameObject); // Destroy the game object if it's not the singleton instance
        }
    }

    public void SetScore(int value)
    {
        _score = value; // Set the current score
    }

    public int GetScore()
    {
        return _score; // Get the current score
    }

    public void IncrementScore()
    {
        _score++; // Increment the current score
        _scoreText.text = _score.ToString(); // Update the score text UI
    }

    public bool IsGameOver()
    {
        return _isGameOver; // Return the game over state
    }

    public void SetGameOverState(bool state)
    {
        _isGameOver = state; // Set the game over state
    }

    public void StopGameIfOver()
    {
        _scoreTextResult.text = "Score: " + _score.ToString(); // Update the score text UI
        _isGameOver = true; // Mark the game as over
        LoadHighestScore(); // Load the highest score at the start
        SaveHighestScore(); // Save the highest score if applicable
        SoundController.Instance.PlaySFX(Constants.GameOverSound); // Play game over sound
        _gameOverPanel.SetActive(true); // Show the game over panel
        Time.timeScale = 0; // Stop the game time
    }

    private void LoadHighestScore()
    {
        _highestScore = PlayerPrefs.GetInt(ConstantsPlayerPrefs.HighestScore, 0); // Load the highest score from PlayerPrefs
    }

    private void SaveHighestScore()
    {
        if (_score > _highestScore) // Check if current score is higher than the highest score
        {
            _highestScore = _score; // Update the highest score
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.HighestScore, _highestScore); // Save the highest score to PlayerPrefs
            PlayerPrefs.Save(); // Save the changes
            UpdateHighestScoreText(); // Update UI for highest score
        }
    }

    private void UpdateHighestScoreText()
    {
        _highestScoreText.text = "Highest Score: " + _highestScore.ToString(); // Update the UI text for highest score
    }

    public void Replay()
    {
        ResetGame(); // Reset the game state
        StartGame(); // Start a new game
    }

    private void ShowTapToPlay()
    {
        _tapToPlayPanel.SetActive(true); // Show the tap to play panel
        _gameOverPanel.SetActive(false); // Hide the game over panel
    }

    public void StartGame()
    {
        _tapToPlayPanel.SetActive(false); // Hide the tap to play panel
        _gameOverPanel.SetActive(false); // Hide the game over panel
        _scoreText.gameObject.SetActive(true); // Show the score text UI
        _spawnItems.StartSpawning(); // Start spawning items in the game
        Time.timeScale = 1; // Resume the game time
    }

    private void ResetGame()
    {
        _score = 0; // Reset the current score
        _isGameOver = false; // Reset the game over state
        _scoreText.text = _score.ToString(); // Update score text UI
        Time.timeScale = 0; // Pause the game time
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from scene loaded event
    }
}

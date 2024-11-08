using Melozing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameController : ManualSingletonMono<GameController>
{
    [SerializeField] private Text _scoreText; // Text UI to display current score
    [SerializeField] private Text _scoreTextResult; // Text UI to display current score
    [SerializeField] private Text _highestScoreText; // Text UI to display highest score

    [SerializeField] private GameObject _layoutStartGame;
    [SerializeField] private GameObject _layoutPopup;
    [SerializeField] private GameObject _UIInGame; // Panel to show when game is over
    [SerializeField] private GameObject _gameOverPanel; // Panel to show when game is over
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _tapToPlayPanel; // Panel for starting the game
    [SerializeField] private SpawnItems _spawnItems; // Reference to the item spawning script

    private int _score; // Current score of the player
    private int _highestScore; // Highest score achieved by the player
    private bool _isGameOver; // Flag to check if the game is over
    private bool _isPause; // Flag to check if the game is over

    private void Start()
    {
        LoadHighestScore();
        UpdateHighestScoreText();
        ShowTapToPlay();
    }

    public int GetScore() => _score;
    public bool IsGameOver() => _isGameOver;
    public bool IsGamePause() => _isPause;

    public void IncrementScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
    }

    private void LoadHighestScore() => _highestScore = PlayerPrefs.GetInt(ConstantsPlayerPrefs.HighestScore, 0);

    private void SaveHighestScore()
    {
        if (_score > _highestScore)
        {
            _highestScore = _score;
            PlayerPrefs.SetInt(ConstantsPlayerPrefs.HighestScore, _highestScore);
            PlayerPrefs.Save();
        }
        UpdateHighestScoreText();
    }

    private void UpdateHighestScoreText() => _highestScoreText.text = "Highest Score: " + _highestScore;

    private void ShowTapToPlay()
    {
        SetGameObjectsActive(false, _layoutPopup, _scoreText.gameObject, _UIInGame, _gameOverPanel, _pausePanel);
        SetGameObjectsActive(true, _layoutStartGame, _tapToPlayPanel);
    }

    public void StartGame()
    {
        SetGameObjectsActive(false, _layoutStartGame, _tapToPlayPanel, _gameOverPanel, _layoutPopup);
        SetGameObjectsActive(true, _UIInGame, _scoreText.gameObject);

        _spawnItems.StartSpawning();
        Time.timeScale = 1;
    }

    private void SetGameObjectsActive(bool isActive, params GameObject[] objects)
    {
        foreach (var obj in objects)
            obj.SetActive(isActive);
    }

    private void ResetGame()
    {
        _score = 0;
        _isGameOver = false;
        _scoreText.text = _score.ToString();
        Time.timeScale = 1;
    }

    public void StopGameIfOver()
    {
        _scoreTextResult.text = "Score: " + _score;
        _isGameOver = true;
        SaveHighestScore();

        SoundController.Instance.PlaySFX(Constants.GameOverSound);
        SetGameObjectsActive(true, _layoutPopup, _gameOverPanel);
        Time.timeScale = 0;
    }

    public void Replay()
    {
        SoundController.Instance.PlaySFX(Constants.PopButton);
        ResetGame();
        StartGame();
    }

    public void PauseGame()
    {
        if (_isGameOver) return;
        _isPause = true;
        ShowPausePanel(true);
        Time.timeScale = 0;
    }

    private void ShowPausePanel(bool isShow) => SetGameObjectsActive(isShow, _layoutPopup, _pausePanel);

    public void ResumeGame()
    {
        SoundController.Instance.PlaySFX(Constants.PopButton);
        _isPause = false;
        ShowPausePanel(false);
        Time.timeScale = 1;
    }

    public void GoToHome()
    {
        SoundController.Instance.PlaySFX(Constants.PopButton);
        ResetGame();
        SceneManager.LoadScene("MainMenu");
    }

}
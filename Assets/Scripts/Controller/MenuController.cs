using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void SetEasyDifficulty()
    {
        GameSettings.Instance.SetDifficulty(GameSettings.Difficulty.Easy);
        SceneManager.LoadScene("Gameplay");
    }

    public void SetMediumDifficulty()
    {
        GameSettings.Instance.SetDifficulty(GameSettings.Difficulty.Medium);
        SceneManager.LoadScene("Gameplay");
    }

    public void SetHardDifficulty()
    {
        GameSettings.Instance.SetDifficulty(GameSettings.Difficulty.Hard);
        SceneManager.LoadScene("Gameplay");
    }
}

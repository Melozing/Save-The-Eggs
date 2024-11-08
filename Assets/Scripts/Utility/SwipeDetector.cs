using UnityEngine;

public class MobileBackButtonHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackButton();
        }
    }

    private void HandleBackButton()
    {
        if (!GameController.Instance.IsGamePause())
        {
            GameController.Instance.PauseGame();
        }
        else
        {
            Application.Quit();
        }
    }
}

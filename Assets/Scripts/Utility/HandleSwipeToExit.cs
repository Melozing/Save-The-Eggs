using UnityEngine;

public class HandleSwipeToExit : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;           // Main menu UI to be shown/hidden
    [SerializeField] private GameObject popupWarningUI;   // Popup warning UI to show before exit

    private bool isPopupShown = false;                    // Flag to check if warning popup is active

    private void Update()
    {
        // Check if the user swipes or presses the back button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleBackButton();
        }
    }

    private void HandleBackButton()
    {
        // If popup is not shown, show it; otherwise, exit
        if (!isPopupShown)
        {
            ShowWarningPopup();
        }
        else
        {
            ExitApp();
        }
    }

    private void ShowWarningPopup()
    {
        if (popupWarningUI)
        {
            popupWarningUI.SetActive(true);
            menuUI.SetActive(false);
            isPopupShown = true;
        }
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void SetIsPopupShown(bool state)
    {
        isPopupShown = state;
    }
}

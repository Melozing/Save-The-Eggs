using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private Text messageText;
    [SerializeField] private Button closeButton;

    public virtual void Setup(string message, System.Action onClose = null)
    {
        if (messageText != null)
        {
            messageText.text = message;
        }

        if (closeButton != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() =>
            {
                onClose?.Invoke();
                PopupManager.Instance.CloseCurrentPopup();
            });
        }
    }
}

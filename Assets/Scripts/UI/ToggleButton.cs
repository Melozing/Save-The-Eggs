using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private ToggleType toggleType;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Button toggleButton;

    private bool isOn;

    private void Start()
    {
        isOn = PlayerPrefs.GetInt(toggleType.ToString(), 1) == 1;

        UpdateButtonImage();

        toggleButton.onClick.AddListener(Toggle);
    }

    private void Toggle()
    {
        isOn = !isOn;
        UpdateButtonImage();

        PlayerPrefs.SetInt(toggleType.ToString(), isOn ? 1 : 0);
        PlayerPrefs.Save();

        switch (toggleType)
        {
            case ToggleType.Music:
                SoundController.Instance.ToggleMusic(isOn);
                break;
            case ToggleType.SFX:
                SoundController.Instance.ToggleSFX(isOn);
                break;
            case ToggleType.Vibration:
                VibrationController.Instance.ToggleVibration(isOn);
                break;
        }
    }

    private void UpdateButtonImage()
    {
        toggleButton.image.sprite = isOn ? onSprite : offSprite;
    }
}

public enum ToggleType
{
    Music,
    SFX,
    Vibration
}

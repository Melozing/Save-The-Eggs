using JS.Utils;
using UnityEngine;

public class VibrationController : ManualSingletonMono<VibrationController>
{
    private bool isVibrationEnabled;

    private void Start()
    {
        isVibrationEnabled = PlayerPrefs.GetInt(ConstantsPlayerPrefs.VibrationEnabled, 1) == 1;
    }

    public void ToggleVibration(bool isEnabled)
    {
        isVibrationEnabled = isEnabled;

        PlayerPrefs.SetInt(ConstantsPlayerPrefs.VibrationEnabled, isVibrationEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void Vibrate()
    {
        if (isVibrationEnabled && SystemInfo.supportsVibration)
        {
            Handheld.Vibrate();
        }
    }

    public bool IsVibrationEnabled()
    {
        return isVibrationEnabled;
    }
}

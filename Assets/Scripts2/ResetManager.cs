using UnityEngine;
using UnityEngine.UI;

public class ResetManager : MonoBehaviour
{
    public Button resetButton;
    public ButtonManager[] buttonManagers;
    public bool resetFlag = false;
    public bool resetSimulator = false;
    public bool restCharacter = false;

    private void Start()
    {
        if (resetButton != null && buttonManagers != null && buttonManagers.Length > 0)
        {
            resetButton.onClick.AddListener(ResetAll);
        }
    }

    private void ResetAll()
    {
        foreach (var buttonManager in buttonManagers)
        {
            if (buttonManager != null)
            {
                buttonManager.ResetToDefault();
            }
        }

        resetFlag = true;
        resetSimulator = true;
        restCharacter = true;
    }
}



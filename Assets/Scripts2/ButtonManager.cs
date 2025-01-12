using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    public enum Mode
    {
        Start,
        Goal,
        Pass,
        Obstacle,
        Item
    }

    private Mode currentMode = Mode.Pass;

    private void Start()
    {
        UpdateButtonMode();
        button.onClick.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        currentMode = (Mode)(((int)currentMode + 1) % 5);
        UpdateButtonMode();
    }

    private void UpdateButtonMode()
    {
        switch (currentMode)
        {
            case Mode.Start:
                button.image.color = Color.green;
                buttonText.text = "Start";
                break;

            case Mode.Goal:
                button.image.color = new Color(1f, 0.5f, 0f); // Orange
                buttonText.text = "Goal";
                break;

            case Mode.Pass:
                button.image.color = Color.white;
                buttonText.text = "Pass";
                break;

            case Mode.Obstacle:
                button.image.color = Color.red;
                buttonText.text = "Obstacle";
                break;

            case Mode.Item:
                button.image.color = Color.yellow;
                buttonText.text = "Item";
                break;
        }
    }

    public Mode GetCurrentMode()
    {
        return currentMode;
    }

    public void ResetToDefault()
    {
        currentMode = Mode.Pass;
        UpdateButtonMode();
    }
}



using UnityEngine;
using UnityEngine.UI;

public class BMS : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    public enum Mode
    {
        Start,
        Goal,
        Pass,
        Obstacle
    }

    private Mode currentMode = Mode.Pass;

    private void Start()
    {
        UpdateButtonMode();
        button.onClick.AddListener(ChangeMode);
    }

    private void ChangeMode()
    {
        currentMode = (Mode)(((int)currentMode + 1) % 4);
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
                button.image.color = new Color(1f, 0.5f, 0f); // オレンジ
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
        }
    }

    public Mode GetCurrentMode()
    {
        return currentMode;
    }
}

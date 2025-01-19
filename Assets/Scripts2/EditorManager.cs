using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントの名前空間

public class EditorManager : MonoBehaviour
{
    private ButtonManager[,] bmsButtons;

    public string modes = "";
    public InputField inputField;
    public Button submitButton;
    public string Text_to_Send = "";
    private string backgroundOrder = "Also, you will answer the coordinates of the start point, the goal point, the obstacles and the items and provide the output in the requested format using directions: 'Up', 'Down', 'Left', and 'Right' with natural number. And, you cannot navigate out of the given field.\n\n"
                                    + "You must not answer anything except for what I ordered.";
    private string representive = "Each element is represented by letter:\n"
                                + "S:the starting point\n"
                                + "G:the goal point\n"
                                + "O:the obstacle to avoid\n"
                                + "P:the path to go\n"
                                + "I:the item to get\n";
    private string example = "Expected output format example:\n"
                            + "the coordinates\n"
                            + "The start point:(0,0)\n"
                            + "The goal point:(4,4)\n"
                            + "The obstacles:(2,2)and(3,3)\n"
                            + "The items:(1,1)and(4,3)\n"
                            + "the output\n"
                            + "Right 3\n"
                            + "Up 4\n"
                            + "Left1\n"
                            + "Down 2\n";
    
    private GameObject resetButton;
    private ResetManager resetManager;
    private bool isFirstPromptSubmitted = false;
    public bool submitToChatGPT = false;
    private GameObject simulator;
    private SimulatorManager simulatorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 5x5の2次元配列を初期化
        bmsButtons = new ButtonManager[5, 5];

        // 5x5のボタンオブジェクトを検索して配列に格納
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                bmsButtons[i, j] = GameObject.Find("Button" + i + "_" + j).GetComponent<ButtonManager>();
            }
        }

        resetButton = GameObject.Find("ResetButton");
        resetManager = resetButton.GetComponent<ResetManager>();

        simulator = GameObject.Find("Simulator");
        simulatorManager = simulator.GetComponent<SimulatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetManager.resetFlag)
        {
            resetManager.resetFlag = false;
            inputField.text = "";
            modes = "";
            Text_to_Send = "";
            isFirstPromptSubmitted = false;
        }

        if (simulatorManager.isHitObstacle)
        {
            Text_to_Send = "The path you suggeted is blocked by the obstacle at (" + simulatorManager.posRow + ", " + simulatorManager.posCol + ") when the movement from the start was " + string.Join(", ", simulatorManager.moves);
            //Debug.Log(Text_to_Send);
            FindAnyObjectByType<DebugConsoleUI>().Log("From Unity to ChatGPT: " + Text_to_Send);
            simulatorManager.isHitObstacle = false;
            submitToChatGPT = true;
        }
        else if (simulatorManager.isGetOutOfField)
        {
            Text_to_Send = "The path you suggeted is out of the field when the movement from the start was " + string.Join(", ", simulatorManager.moves);
            //Debug.Log(Text_to_Send);
            FindAnyObjectByType<DebugConsoleUI>().Log("From Unity to ChatGPT: " + Text_to_Send);
            simulatorManager.isGetOutOfField = false;
            submitToChatGPT = true;
        }
        else if (simulatorManager.isNotGoalReached)
        {
            Text_to_Send = "The path you suggeted is not reached to the goal point when the movement from the start was " + string.Join(", ", simulatorManager.moves);
            //Debug.Log(Text_to_Send);
            FindAnyObjectByType<DebugConsoleUI>().Log("From Unity to ChatGPT: " + Text_to_Send);
            simulatorManager.isNotGoalReached = false;
            submitToChatGPT = true;
        }
    }

    public void submit()
    {
        if (!isFirstPromptSubmitted)
        {
            isFirstPromptSubmitted = true;
            // モードの出力
            PrintModes();
            // テキストの出力
            Edit_FirstPrompt();
            inputField.text = "";
            submitToChatGPT = true;
        }
        else
        {
            sendText();
            submitToChatGPT = true;
        }
        
    }

    public void PrintModes()
    {
        //string modes = "";
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Start)
                {
                    modes += "S";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Goal)
                {
                    modes += "G";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Pass)
                {
                    modes += "P";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                {
                    modes += "O";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Item)
                {
                    modes += "I";
                }
            }
            modes += "\n"; // 各行の出力を改行
        }
        //Debug.Log(modes.Trim());
    }

    public void Edit_FirstPrompt()
    {
        string inputFieldText = inputField.text;
        FindAnyObjectByType<DebugConsoleUI>().Log("You: " + inputFieldText);
        //combine inputFieldText and modes, changing the line below
        Text_to_Send = inputFieldText + " in the given field \n" + backgroundOrder + "\n\n" + example + "\n" + representive + "\n" +  "given the field\n" + modes/* + "\n" + note*/;
        //Debug.Log(Text_to_Send);
    }

    public void sendText()
    {
        Text_to_Send = inputField.text;
        //SSDebug.Log(Text_to_Send);
    }
}

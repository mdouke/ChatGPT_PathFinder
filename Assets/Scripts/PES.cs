using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントの名前空間

public class PES : MonoBehaviour
{
    public InputField inputField;
    public Button checkButton;

    public GameObject fieldManager;
    private FMS fms;

    public GameObject simulator;
    private SMS sms;

    private string modes;

    public string Text_to_Send;

    private string backgroundOrder = "Also, you will answer the coordinates of the start point, the goal point and the obstacles and provide the output in the requested format using directions: 'Up', 'Down', 'Left', and 'Right' with natural number. And, you cannot navigate out of the given field.\n\n"
                                    + "You must not answer anything except for what I ordered.";
    private string representive = "Each element is represented by letter:\n"
                                + "S:the starting point\n"
                                + "G:the goal point\n"
                                + "O:the obstacle to avoid\n"
                                + "P:the path to go\n";
    private string example = "Expected output format example:\n"
                            + "the coordinates\n"
                            //+ "```\n"
                            + "The start point:(0,0)\n"
                            + "The goal point:(4,4)\n"
                            + "The obstacles:(2,2)and(3,3)\n"
                            //+ "```\n"
                            + "the output\n"
                            //+ "```\n"
                            + "Right 3\n"
                            + "Up 4\n"
                            + "Left1\n"
                            + "Down 2\n";
                            //+ "```";
    private string gtf = "given the field:";
    //private string note = "Note: You must navigate the path to the goal point without touching the obstacles";
    void Start()
    {
        fms = fieldManager.GetComponent<FMS>();
        
        simulator = GameObject.Find("Simulator");
        sms = simulator.GetComponent<SMS>();
    }

    void Update()
    {
        //modes = fms.modes;
        //Debug.Log(modes);

        if (sms.isHitObstacle)
        {
            Text_to_Send = "The path you suggeted is blocked by the obstacle at (" + sms.posRow + ", " + sms.posCol + ") when the movement from the start was " + string.Join(", ", sms.moves);
            Debug.Log(Text_to_Send);
            sms.isHitObstacle = false;
        }

        if (sms.isGetOutOfField)
        {
            Text_to_Send = "The path you suggeted is out of the field when the movement from the start was " + string.Join(", ", sms.moves);
            Debug.Log(Text_to_Send);
            sms.isGetOutOfField = false;
        }

        if (sms.isNotGoalReached)
        {
            Text_to_Send = "The path you suggeted is not reached to the goal point when the movement from the start was " + string.Join(", ", sms.moves);
            Debug.Log(Text_to_Send);
            sms.isNotGoalReached = false;
        }
    }

    public void Edit_FirstPrompt()
    {
        string inputFieldText = inputField.text;
        modes = fms.modes;
        //combine inputFieldText and modes, changing the line below
        Text_to_Send = inputFieldText + " in the given field \n" + backgroundOrder + "\n\n" + example + "\n\n" + representive + "\n\n" + gtf + "\n" + modes/* + "\n" + note*/;
        Debug.Log(Text_to_Send);
    }

    public void sendText()
    {
        Text_to_Send = inputField.text;
        Debug.Log(Text_to_Send);
    }
}
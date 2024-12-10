using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントの名前空間

public class PES2 : MonoBehaviour
{
    public InputField inputField;
    public Button checkButton;

    public GameObject fieldManager;
    private FMS fms;

    private string modes;

    public string Text_to_Send;

    private string backgroundOrder = "Also, you will answer the coordinates of the start point, the goal point and the obstacles and provide the output in the requested format using directions: 'Up', 'Down', 'Left', and 'Right' with natural number.\n\n"
                                    + "You must not answer anything except for what I ordered.";
    private string representive = "Each element is represented by letter:\n"
                                + "S:the starting point\n"
                                + "G:the goal point\n"
                                + "O:the obstacle to avoid\n"
                                + "P:the path to go\n";
    private string example = "Expected output format example:\n"
                            + "the coordinates\n"
                            + ",,,\n"
                            + "The start point:(0,0)\n"
                            + "The goal point:(4,4)\n"
                            + "The obstacles:(2,2)and(3,3)\n"
                            + ",,,\n"
                            + "the output\n"
                            + ",,,\n"
                            + "Right3\n"
                            + "Up4\n"
                            + "Left1\n"
                            + "Down2\n"
                            + ",,,";
    private string gtf = "Given the field:";
    //private string note = "Note: You must navigate the path to the goal point without touching the obstacles";
    void Start()
    {
        fms = fieldManager.GetComponent<FMS>();
        
    }

    void Update()
    {
        //modes = fms.modes;
        //Debug.Log(modes);
    }

    public void Edit_FirstPrompt()
    {
        string inputFieldText = inputField.text;
        modes = fms.modes;
        //combine inputFieldText and modes, changing the line below
        Text_to_Send = inputFieldText + "\n" + backgroundOrder + "\n\n" + example + "\n\n" + representive + "\n\n" + gtf + "\n" + modes/* + "\n" + note*/;
        Debug.Log(Text_to_Send);
    }

    public void sendText()
    {
        Text_to_Send = inputField.text;
        Debug.Log(Text_to_Send);
    }
}
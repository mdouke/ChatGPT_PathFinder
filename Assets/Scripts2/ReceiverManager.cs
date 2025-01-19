using UnityEngine;
using System.Linq;

public class ReceiverManager : MonoBehaviour
{
    GameObject chatGPT;
    TestChat3 testChat3;
    private string responseText;
    public bool isResponseChanged = false;
    public string[] outputLines;
    public string[,] outputWords;
    GameObject simulator;
    SimulatorManager simulatorManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chatGPT = GameObject.Find("ChatGPT");
        testChat3 = chatGPT.GetComponent<TestChat3>();

        simulator = GameObject.Find("Simulator");
        simulatorManager = simulator.GetComponent<SimulatorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResponseChanged)
        {
            DebugResponse();
            ExtractResponse();
        }
    }

    public void DebugResponse()
    {
        responseText = testChat3.responseText;
        Debug.Log(responseText);
        isResponseChanged = false;
    }

    //responseTextから必要な情報を取り出す
    public void ExtractResponse()
    {
        //responseTextから必要な情報を取り出す
        //responseTextの中身を全て小文字に変換
        responseText = responseText.ToLower();
        string[] lines = responseText.Split('\n');

        outputLines = new string[lines.Length];
        int outputLinesIndex = 0;
        //lineの中に"the output"があったらその次の行から最後までを取り出す
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("the output"))
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    //Debug.Log(lines[j]);
                    outputLines[outputLinesIndex] = lines[j];
                    outputLinesIndex++;
                }
            }
        }
        
        //outputLinesからnullを取り除く
        outputLines = outputLines.Where(x => x != null).ToArray();
        //outputLinesの中身を表示
        FindAnyObjectByType<DebugConsoleUI>().Log("ChatGPT: " + string.Join(", ", outputLines));
        //outputLines.Length行2列の2次元配列を作成
        outputWords = new string[outputLines.Length, 2];
        //outputLinesの中身をspaceで分解して表示
        for (int i = 0; i < outputLines.Length; i++)
        {
            string[] words = outputLines[i].Split(' ');
            for (int j = 0; j < 2; j++)
            {
                outputWords[i, j] = words[j];
                //Debug.Log("outputWordsの"+ i + "行"+ j + "列目: " + outputWords[i, j]);
            }
        }
        simulatorManager.isOutputGenerated = true;
    }
}

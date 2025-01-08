using UnityEngine;

public class SMS : MonoBehaviour
{
    GameObject receiver;
    RMS rMS;
    public bool isOutputGenerated = false;
    private string[,] outputWords;
    private BMS[,] bmsButtons;
    private int posRow;
    private int posCol;
    //private bool isHitObstacle = false;
    //private string[,] finishedMoves;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receiver = GameObject.Find("Receiver");
        rMS = receiver.GetComponent<RMS>();

        bmsButtons = new BMS[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                bmsButtons[i, j] = GameObject.Find("Button" + i + "_" + j).GetComponent<BMS>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutputGenerated)
        {
            isOutputGenerated = false;
            Simulate();
        }
    }

    public void Simulate()
    {
        //outputWordsから必要な情報を取り出す
        outputWords = rMS.outputWords;
        
        //outputWordsの中身を全て表示
        /*
        for (int i = 0; i < outputWords.GetLength(0); i++)
        {
            for (int j = 0; j < outputWords.GetLength(1); j++)
            {
                Debug.Log(outputWords[i, j]);
            }
        }*/
        //outputWordsが何行何列かを表示
        //Debug.Log(outputWords.GetLength(0) + " " + outputWords.GetLength(1));

        searchStart();
        movement();

    }

    void searchStart()
    {
        // スタートボタンを探して、その座標を取得
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Start)
                {
                    posCol = j;
                    posRow = i;
                    break;
                }
            }
        }
    }

    void movement()
    {
        for (int i = 0; i < outputWords.GetLength(0); i++)
        {
            //outputWords[i,1]をint型に変換
            int num = int.Parse(outputWords[i, 1]);
            for (int j = 0; j < num; j++)
            {
                if (outputWords[i, 0] == "right")
                {
                    posCol++;
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == BMS.Mode.Obstacle)
                    {
                        //isHitObstacle = true;
                        Debug.Log("Hit obstacle at (" + posRow + ", " + posCol + ")");
                        break;
                    }
                } else if (outputWords[i, 0] == "left")
                {
                    posCol--;
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == BMS.Mode.Obstacle)
                    {
                        //isHitObstacle = true;
                        Debug.Log("Hit obstacle at (" + posRow + ", " + posCol + ")");
                        break;
                    }
                } else if (outputWords[i, 0] == "up")
                {
                    posRow--;
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == BMS.Mode.Obstacle)
                    {
                        //isHitObstacle = true;
                        Debug.Log("Hit obstacle at (" + posRow + ", " + posCol + ")");
                        break;
                    }
                } else if (outputWords[i, 0] == "down")
                {
                    posRow++;
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == BMS.Mode.Obstacle)
                    {
                        //isHitObstacle = true;
                        Debug.Log("Hit obstacle at (" + posRow + ", " + posCol + ")");
                        break;
                    }
                }
            }
        }
    }
}

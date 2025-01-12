using System;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorManager : MonoBehaviour
{
    GameObject receiver;
    ReceiverManager receiverManager;
    public bool isOutputGenerated = false;
    private string[,] outputWords;
    private ButtonManager[,] bmsButtons;
    public int posRow;
    public int posCol;
    public List<string> moves = new List<string>();
    public bool isHitObstacle = false;
    public bool isGetOutOfField = false;
    private List<Tuple<int, int>> goalPositions = new List<Tuple<int, int>>();
    public bool isGoalReached = false;
    public bool isNotGoalReached = false;
    private GameObject resetButton;
    private ResetManager resetManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receiver = GameObject.Find("Receiver");
        receiverManager = receiver.GetComponent<ReceiverManager>();

        bmsButtons = new ButtonManager[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                bmsButtons[i, j] = GameObject.Find("Button" + i + "_" + j).GetComponent<ButtonManager>();
            }
        }

        resetButton = GameObject.Find("ResetButton");
        resetManager = resetButton.GetComponent<ResetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutputGenerated)
        {
            isOutputGenerated = false;
            Simulate();
        }

        if (resetManager.resetSimulator)
        {
            resetManager.resetSimulator = false;
            moves.Clear();
            isHitObstacle = false;
            isGetOutOfField = false;
            isGoalReached = false;
            isNotGoalReached = false;
        }
    }

    public void Simulate()
    {
        //outputWordsから必要な情報を取り出す
        outputWords = receiverManager.outputWords;
        
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
        searchGoal();
        moves.Clear();
        movement();

    }

    void searchStart()
    {
        // スタートボタンを探して、その座標を取得
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Start)
                {
                    posCol = j;
                    posRow = i;
                    break;
                }
            }
        }
    }

    void searchGoal()
    {
        // ゴールボタンを探して、その座標を取得
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Goal)
                {
                    goalPositions.Add(new Tuple<int, int>(i, j));
                }
            }
        }
    }

    void movement()
    {
        //List<string> moves = new List<string>();
        //bool isHitObstacle = false;
        for (int i = 0; i < outputWords.GetLength(0); i++)
        {
            //outputWords[i,1]をint型に変換
            int num = int.Parse(outputWords[i, 1]);
            for (int j = 0; j < num; j++)
            {
                if (outputWords[i, 0] == "right")
                {
                    posCol++;
                    moves.Add("Right");

                    if (posCol >= 5)//フィールド外に出たかどうかを判定
                    {
                        isGetOutOfField = true;
                        break;
                    }

                    if (bmsButtons[posRow, posCol].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                    {
                        isHitObstacle = true;
                        break;
                    }
                } else if (outputWords[i, 0] == "left")
                {
                    posCol--;
                    moves.Add("Left");

                    if (posCol < 0)//フィールド外に出たかどうかを判定
                    {
                        isGetOutOfField = true;
                        break;
                    }

                    if (bmsButtons[posRow, posCol].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                    {
                        isHitObstacle = true;
                        break;
                    }
                } else if (outputWords[i, 0] == "up")
                {
                    posRow--;
                    moves.Add("Up");

                    if (posRow < 0)//フィールド外に出たかどうかを判定
                    {
                        isGetOutOfField = true;
                        break;
                    }
                    
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                    {
                        isHitObstacle = true;
                        break;
                    }
                } else if (outputWords[i, 0] == "down")
                {
                    posRow++;
                    moves.Add("Down");

                    if (posRow >= 5)//フィールド外に出たかどうかを判定
                    {
                        isGetOutOfField = true;
                        break;
                    }
                    
                    if (bmsButtons[posRow, posCol].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                    {
                        isHitObstacle = true;
                        break;
                    }
                }
            }
            if (isHitObstacle || isGetOutOfField)
            {
                //Debug.Log("Hit obstacle at (" + posRow + ", " + posCol + ")");
                break;
            }
        }
        //Debug.Log("Suggested moves: " + string.Join(", ", moves));

        // ゴールに到達したかどうかを判定
        if (isHitObstacle != true && isGetOutOfField != true)
        {
            foreach (var goalPosition in goalPositions)
            {
                if (posRow == goalPosition.Item1 && posCol == goalPosition.Item2)
                {
                    isGoalReached = true;
                    isNotGoalReached = false;
                    Debug.Log("Goal reached at (" + posRow + ", " + posCol + ")");
                    break;
                } else
                {
                    isGoalReached = false;
                    isNotGoalReached = true;
                }
            }
        }
        
    }
}


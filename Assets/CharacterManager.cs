using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private ButtonManager[,] bmsButtons;
    private GameObject[,] tiles;
    private bool isIdle;
    private bool isOnStart;
    private Vector3 startPosition;
    private GameObject simulator;
    private SimulatorManager simulatorManager;
    private GameObject resetButton;
    private ResetManager resetManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bmsButtons = new ButtonManager[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                bmsButtons[i, j] = GameObject.Find("Button" + i + "_" + j).GetComponent<ButtonManager>();
            }
        }
        tiles = new GameObject[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                tiles[i, j] = GameObject.Find("Tile" + i + "_" + j);
            }
        }
        isIdle = true;

        simulator = GameObject.Find("Simulator");
        simulatorManager = simulator.GetComponent<SimulatorManager>();

        resetButton = GameObject.Find("ResetButton");
        resetManager = resetButton.GetComponent<ResetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetManager.restCharacter)
        {
            isIdle = true;
            isOnStart = false;
            resetManager.restCharacter = false;
        }

        //Debug.Log(isIdle);
        if (isIdle)
        {
            transform.localPosition = new Vector3(0, 0, -1);
            searchStart();     
            //isIdle = false;
        }
        else if (isOnStart)
        {
            transform.localPosition = startPosition;
        }

        if (simulatorManager.isGoalReached)
        {
            // コルーチンを開始
            StartCoroutine(PerformMoves());
        }
    }

    void searchStart()
    {
        // スタートボタンを探す
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Start)
                {
                    // スタートボタンを見つけたら、そのボタンの座標を取得
                    startPosition = new Vector3(1 * j, -1 * i, -1);
                    Debug.Log("Start position: " + startPosition);
                    isIdle = false;
                    isOnStart = true;
                    break;
                }
            }
        }
    }

    private IEnumerator PerformMoves()
    {
        for (int i = 0; i < simulatorManager.moves.Count; i++)
        {
            switch (simulatorManager.moves[i])
            {
                case "Up":
                    yield return MoveOverTime(new Vector3(0, 1, -1), 1f);
                    break;
                case "Down":
                    yield return MoveOverTime(new Vector3(0, -1, -1), 1f);
                    break;
                case "Left":
                    yield return MoveOverTime(new Vector3(-1, 0, -1), 1f);
                    break;
                case "Right":
                    yield return MoveOverTime(new Vector3(1, 0, -1), 1f);
                    break;
            }
        }
    }

    private IEnumerator MoveOverTime(Vector3 direction, float duration)
    {
        Vector3 start = transform.localPosition;                // 現在の位置
        Vector3 end = start + direction;                        // 目標位置
        float elapsedTime = 0f;                                 // 経過時間

        while (elapsedTime < duration)
        {
            // 線形補間（Lerp）で位置を移動
            transform.localPosition = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;                      // フレームごとに時間を加算
            yield return null;                                  // 次のフレームまで待機
        }

        transform.localPosition = end; // 最終位置を正確に設定
    }

}

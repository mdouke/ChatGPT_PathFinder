using UnityEngine;

public class FMS : MonoBehaviour
{
    private BMS[,] bmsButtons;

    public string modes = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 5x5の2次元配列を初期化
        bmsButtons = new BMS[5, 5];

        // 5x5のボタンオブジェクトを検索して配列に格納
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
       
    }

    public void PrintModes()
    {
        //string modes = "";
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Start)
                {
                    modes += "S";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Goal)
                {
                    modes += "G";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Pass)
                {
                    modes += "P";
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Obstacle)
                {
                    modes += "O";
                }
            }
            modes += "\n"; // 各行の出力を改行
        }
        Debug.Log(modes.Trim());
    }
}

using UnityEngine;

public class CMS : MonoBehaviour
{
    private BMS[,] bmsButtons;
    private GameObject[,] tiles;
    private bool isIdle;
    private bool isOnStart;
    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bmsButtons = new BMS[5, 5];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                bmsButtons[i, j] = GameObject.Find("Button" + i + "_" + j).GetComponent<BMS>();
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
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void searchStart()
    {
        // スタートボタンを探す
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Start)
                {
                    // スタートボタンを見つけたら、そのボタンの座標を取得
                    startPosition = new Vector3(100 * j, -100 * i, -1);
                    Debug.Log("Start position: " + startPosition);
                    isIdle = false;
                    isOnStart = true;
                    break;
                }
            }
        }
    }

}

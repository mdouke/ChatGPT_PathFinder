using UnityEngine;

public class TMS : MonoBehaviour
{
    private BMS[,] bmsButtons;

    private GameObject[,] tiles;
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Start)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Goal)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0);
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Pass)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.white;
                }
                else if (bmsButtons[i, j].GetCurrentMode() == BMS.Mode.Obstacle)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.red;
                }
            }
        }
    }
}

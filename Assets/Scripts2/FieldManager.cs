using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private ButtonManager[,] bmsButtons;

    private GameObject[,] tiles;
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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Start)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.green;
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Goal)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = new Color(1, 0.5f, 0);
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Pass)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.white;
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Obstacle)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.red;
                }
                else if (bmsButtons[i, j].GetCurrentMode() == ButtonManager.Mode.Item)
                {
                    tiles[i, j].GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
    }
}

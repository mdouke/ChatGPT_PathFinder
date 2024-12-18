using UnityEngine;

public class SMS : MonoBehaviour
{
    GameObject receiver;
    RMS rMS;
    public bool isOutputGenerated = false;
    GameObject fieldManager;
    FMS fMS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        receiver = GameObject.Find("Receiver");
        rMS = receiver.GetComponent<RMS>();

        fieldManager = GameObject.Find("FieldManager");
        fMS = fieldManager.GetComponent<FMS>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isOutputGenerated)
        {
            Simulate();
            isOutputGenerated = false;
        }
    }

    public void Simulate()
    {
        //outputWordsから必要な情報を取り出す
        string[,] outputWords = rMS.outputWords;
        
        //outputWordsの中身を全て表示
        for (int i = 0; i < outputWords.GetLength(0); i++)
        {
            for (int j = 0; j < outputWords.GetLength(1); j++)
            {
                //Debug.Log(outputWords[i, j]);
            }
        }
        
        //modesを取得
        string modes = fMS.modes;
        //Debug.Log(modes);        
    }
}

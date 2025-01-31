using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DebugConsoleUI : MonoBehaviour
{
    [SerializeField] private Text debugText; // ログ表示用のText
    [SerializeField] private ScrollRect scrollRect; // ScrollRectへの参照
    private StringBuilder debugLogBuilder = new StringBuilder();
    private const string StartLog = "Unity: Please set the field and submit a command.";

    void Start()
    {
        debugLogBuilder.AppendLine(StartLog);
        UpdateDebugText();
    }

    public void Log(string message)
    {
        debugLogBuilder.AppendLine(message); // メッセージを追記
        UpdateDebugText();
        ScrollToBottom(); // スクロール位置を更新
    }

    private void UpdateDebugText()
    {
        if (debugText != null)
        {
            debugText.text = debugLogBuilder.ToString();

            // 高さを再計算して更新
            Canvas.ForceUpdateCanvases();
            RectTransform textRectTransform = debugText.GetComponent<RectTransform>();
            float preferredHeight = debugText.preferredHeight;
            textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, preferredHeight);
        }
    }

    private void ScrollToBottom()
    {
        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases(); // レイアウトの更新を強制
            scrollRect.verticalNormalizedPosition = 0f; // スクロール位置を一番下に設定
        }
    }
}



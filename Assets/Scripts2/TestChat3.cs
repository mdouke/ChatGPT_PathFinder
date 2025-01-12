using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;  // UIコンポーネントの名前空間
using UnityEngine.Networking;

public class TestChat3 : MonoBehaviour
{
    #region 必要なクラスの定義など
    [System.Serializable]
    public class MessageModel
    {
        public string role;
        public string content;
    }
    [System.Serializable]
    public class CompletionRequestModel
    {
        public string model;
        public List<MessageModel> messages;
    }

    [System.Serializable]
    public class ChatGPTRecieveModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public MessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
    #endregion

    private GameObject editor;
    private EditorManager editorManager;
    private GameObject receiver;
    private ReceiverManager receiverManager;
    public string responseText;

    private MessageModel assistantModel = new()
    {
        role = "system",
        content = "You are a navigator"
    };
    private readonly string apiKey = "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";
    private List<MessageModel> communicationHistory = new();

    void Start()
    {
        communicationHistory.Add(assistantModel);
        
        editor = GameObject.Find("Editor");
        editorManager = editor.GetComponent<EditorManager>();

        receiver = GameObject.Find("Receiver");
        receiverManager = receiver.GetComponent<ReceiverManager>();
    }

    void Update()
    {
        if (editorManager.submitToChatGPT)
        {
            editorManager.submitToChatGPT = false;
            MessageSubmit(editorManager.Text_to_Send);
            //Debug.Log("MessageIsSent");
        }
    }

    private void Communication(string newMessage, Action<MessageModel> result)
    {
        Debug.Log(newMessage);
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage
        });

        var apiUrl = "https://api.openai.com/v1/chat/completions";
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-4o",
                messages = communicationHistory
            }, true);
        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };
        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                       operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(operation.webRequest.error);
                throw new Exception();
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                //Debug.Log(responseObject.choices[0].message.content);
                responseText = responseObject.choices[0].message.content;
                receiverManager.isResponseChanged = true;
            }
            request.Dispose();
        };
    }

    public void MessageSubmit(string sendMessage)
    {
        Communication(sendMessage, (result) =>
        {
            Debug.Log(result.content);
        });
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    private string _message;

    public static Chat Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        
        Destroy(gameObject);
    }

    public void SetMessage(string message)
    {
        _message = message;
    }

    public void Send()
    {
        MultiplayerManager.Instance.SendMessage("msg", _message);
    }

    [SerializeField] private Text _text;

    public void ApplyMessage(string message)
    {
        _text.text += $"[{DateTime.Now.ToString("HH:mm:ss")}] {message}\n";
    }
}

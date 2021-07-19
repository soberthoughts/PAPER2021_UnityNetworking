using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Collections;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI.Connection;
using MLAPI.Messaging;

public class PlayerSetUp : NetworkBehaviour
{
    public Text namePrefab;
    [HideInInspector] public Text chatLabel;
    public Transform chatPosition;
    [HideInInspector] public string chatTextBox = "";

    public NetworkVariable<string> playerMessage = new NetworkVariable<string>("PLAYER");

    public override void NetworkStart()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        chatLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        chatLabel.transform.SetParent(canvas.transform);
    }

    private void Awake()
    {
        if (IsClient)
        {
            playerMessage.OnValueChanged += MessageChanged;
        }
    }

    private void Update()
    {
        Vector3 chatLabelPos = Camera.main.WorldToScreenPoint(chatPosition.position);
        chatLabel.transform.position = chatLabelPos;
    }

    private void OnGUI()
    {
        if (IsLocalPlayer)
        {
            chatTextBox = GUI.TextField(new Rect(210, 25, 160, 30), chatTextBox);
            if (GUI.Button(new Rect(380, 25, 160, 30), "Send"))
            {
                    MessageChangedServerRpc(chatTextBox);
            }
        }
    }

    [ServerRpc]
    public void MessageChangedServerRpc(string _playerMessage)
    {
        playerMessage.Value = _playerMessage;
        chatLabel.text = playerMessage.Value;
        MessageChangedClientRpc(_playerMessage);
    }

    [ClientRpc]
    public void MessageChangedClientRpc(string _playerMessage)
    {
        playerMessage.Value = _playerMessage;
        chatLabel.text = playerMessage.Value;
    }


    void MessageChanged(string oldMessage, string _playerMessage)
    { 
        Debug.Log(oldMessage);
        Debug.Log(_playerMessage);
    }

}








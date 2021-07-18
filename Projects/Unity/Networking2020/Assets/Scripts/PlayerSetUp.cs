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
    [HideInInspector] public Text nameLabel;
    public Transform namePosition;
    [HideInInspector] public string nameTextBox = "";

    public NetworkVariable<string> playerName = new NetworkVariable<string>("PLAYER");

    public override void NetworkStart()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform);
    }

    private void Awake()
    {
        if (IsClient)
        {
            playerName.OnValueChanged += NameChanged;
        }
    }

    private void Update()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePosition.position);
        nameLabel.transform.position = nameLabelPos;
    }

    private void OnGUI()
    {
        if (IsLocalPlayer)
        {
            nameTextBox = GUI.TextField(new Rect(210, 25, 160, 30), nameTextBox);
            if (GUI.Button(new Rect(380, 25, 160, 30), "Set"))
            {
                    NameChangedServerRpc(nameTextBox);
            }
        }
    }

    [ServerRpc]
    public void NameChangedServerRpc(string _playerName)
    {
        playerName.Value = _playerName;
        nameLabel.text = playerName.Value;
        NameChangedClientRpc(_playerName);
    }

    [ClientRpc]
    public void NameChangedClientRpc(string _playerName)
    {
        playerName.Value = _playerName;
        nameLabel.text = playerName.Value;
    }


    void NameChanged(string oldName, string _playerName)
    { 
        Debug.Log(oldName);
        Debug.Log(_playerName);
    }

}








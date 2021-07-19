using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerChatSetup : MonoBehaviourPun
{
    public Text namePrefab;
    [HideInInspector] public Text nameLabel;
    public Transform namePos;
    [HideInInspector] public string textBoxName = "";

    public string playerName = "Player";

    private void Start()
    {
        GameObject canvas = GameObject.FindWithTag("MainCanvas");
        nameLabel = Instantiate(namePrefab, Vector3.zero, Quaternion.identity) as Text;
        nameLabel.transform.SetParent(canvas.transform);
    }

    private void Update()
    {
        Vector3 nameLabelPos = Camera.main.WorldToScreenPoint(namePos.position);
        nameLabel.transform.position = nameLabelPos;
    }

    private void OnGUI()
    {
        textBoxName = GUI.TextField(new Rect(25, 15, 100, 25), textBoxName);
        if (GUI.Button(new Rect(130, 15, 50, 25), "Send"))
        {
            photonView.RPC("ChangeName", RpcTarget.All, textBoxName);
        }

    }

    [PunRPC]
    public void ChangeName(string _playerName)
    {
        playerName = _playerName;
        nameLabel.text = playerName;
    }

}

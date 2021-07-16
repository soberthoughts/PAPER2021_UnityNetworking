using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class SetUpLocalPlayer : NetworkBehaviour
{
    public Text namePrefab;
    [HideInInspector] public Text nameLabel;
    public Transform namePos;
    public Material playerCarMaterial;
    [HideInInspector] public string textBoxName = "";
    [HideInInspector] public string textBoxColor = "";

    [SyncVar (hook = "OnNameChange")]
    public string playerName = "Player";

    [SyncVar(hook = "OnColorChange")]
    public string playerColor = "#0000FF";

    // Start is called before the first frame update
    private void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<PlayerController>().enabled = true;
            CameraFollow.player = this.gameObject.transform;
        } else
        {
            GetComponent<PlayerController>().enabled = false;
        }

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
        if (isLocalPlayer)
        {
            textBoxName = GUI.TextField(new Rect(25, 15, 100, 25), textBoxName);
            if (GUI.Button(new Rect(130, 15, 35, 25), "Set"))
            {
                CmdChangeName(textBoxName);
            }

            textBoxColor = GUI.TextField(new Rect(170, 15, 100, 25), textBoxColor);
            if (GUI.Button(new Rect(275, 15, 35, 25), "Set"))
            {
                CmdChangeColor(textBoxColor);
            }
        }
    }

    [Command]
    public void CmdChangeName(string _playerName)
    {
        playerName = _playerName;
        nameLabel.text = playerName;
    }

    public void OnNameChange(string _playerName)
    {
        playerName = _playerName;
        nameLabel.text = playerName;
    }

    public void OnColorChange(string _playerColor)
    {
        playerColor = _playerColor;

        Color color;
        ColorUtility.TryParseHtmlString(playerColor, out color);

        playerCarMaterial.SetColor("_Color", color);


    }

    [Command]
    private void CmdChangeColor(string _playerColor)
    {
        playerColor = _playerColor;
        Color color;
        ColorUtility.TryParseHtmlString(playerColor, out color);

        playerCarMaterial.SetColor("_Color", color);

    }


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private InputField nameInput = null;
    [SerializeField] private Button startButton = null;

    private const string PlayerPrefsName = "PlayerName";

    private void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsName))
        {
            return;
        }

        string defaultName = PlayerPrefs.GetString(PlayerPrefsName);
        nameInput.text = defaultName;

        SetPlayerName();
    }

    public void SetPlayerName()
    {
        startButton.interactable = !string.IsNullOrEmpty(nameInput.text);
    }

    public void SavePlayerName()
    {
        string playerName = nameInput.text;
        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(PlayerPrefsName, playerName);
    }
}

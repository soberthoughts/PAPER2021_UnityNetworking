using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject findFriendPanel = null;
    [SerializeField] private GameObject waitingPanel = null;
    [SerializeField] private Text waitingStatusText = null;

    private bool isConnecting = false;

    private const string GameVersion = "1";
    private const int maxPlayers = 2;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void FindFriend()
    {
        isConnecting = true;
        findFriendPanel.SetActive(false);
        waitingPanel.SetActive(true);

        waitingStatusText.text = "Searching for a friend...";

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        } else
        {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Just connected to master.");
        
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        waitingPanel.SetActive(false);
        findFriendPanel.SetActive(true);

        Debug.Log($"Disconnected due to {cause}");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No friends found.");

        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Client successfuly joined the room.");

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if (playerCount != maxPlayers)
        {
            waitingStatusText.text = "Waiting for a friend to join.";
        } else
        {
            Debug.Log("Ready to begin.");
            waitingStatusText.text = "Friend found.";
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            waitingStatusText.text = "Friend found.";
            Debug.Log("Ready to begin.");
            PhotonNetwork.LoadLevel("PUN2_Game");
        }
    }



}










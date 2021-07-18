using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MenuManager : MonoBehaviour
{
    private GameObject menuPanel;
    public GameObject playerUI;
    void Start()
    {
        menuPanel = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HostButton()
    {
        NetworkManager.Singleton.StartHost();
        menuPanel.SetActive(false);
        playerUI.SetActive(true);
    }
    
    public void ClientButton()
    {
        playerUI.SetActive(true);
        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }
}

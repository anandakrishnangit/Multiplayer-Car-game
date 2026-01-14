using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;


public class Networkclienthost : MonoBehaviour
{
    public Button startclient_button;
    public Button starthost_button;
    public Button Startserver_button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startclient_button.onClick.AddListener(Startclient);
        starthost_button.onClick.AddListener(StartHost);
        Startserver_button.onClick.AddListener(StartServer);
    }

    void Startclient()
    {
        NetworkManager.Singleton.StartClient();
    }
    void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }
    void Update()
    {

    }
}

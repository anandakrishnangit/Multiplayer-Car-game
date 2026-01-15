using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NETWORK_BUTTONMANAGER : MonoBehaviour
{
    public Button StartHostButton;
    public Button StartClientButton;
    public Button StartServerButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartHostButton.onClick.AddListener(StartHost);
        StartServerButton.onClick.AddListener(StartServer);
        StartClientButton.onClick.AddListener(StartClient);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
    void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }
}

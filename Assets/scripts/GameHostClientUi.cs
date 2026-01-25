using Unity.Netcode;
using UnityEngine;

public class GameHostClientUi :NetworkBehaviour
{
    public GameObject HostClientUIPanel; 

    public void HostButton()
    {
        HostClientUIPanel.SetActive(false);
    }
    public void ClientButton()
    {
        HostClientUIPanel.SetActive(false);
    }
}

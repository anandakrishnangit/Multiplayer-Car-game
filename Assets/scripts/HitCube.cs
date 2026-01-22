using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HitCube : NetworkBehaviour
{
    public NetworkVariable<int> hostHits = new NetworkVariable<int>(0);
    public NetworkVariable<int> clientHits = new NetworkVariable<int>(0);

    public int winHits = 3;

    private void OnTriggerEnter(Collider other)
    {
        
    
    
        if (!IsServer) return;

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            ulong clientId = other.gameObject.GetComponent<NetworkObject>().OwnerClientId;

            if (clientId == NetworkManager.Singleton.LocalClientId)
            {
                hostHits.Value++;
            }
            else
            {
                clientHits.Value++;
            }

            CheckWin();
        }
    }

    void CheckWin()
    {
        if (hostHits.Value >= winHits)
            GameResult.Instance.SetResultClientRpc("HOST WIN");

        if (clientHits.Value >= winHits)
            GameResult.Instance.SetResultClientRpc("CLIENT WIN");
    }
}

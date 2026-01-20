using JetBrains.Annotations;
using Unity.Netcode;
using UnityEngine;

public class Carhealth : NetworkBehaviour
{
    public int maxhealth = 100;
    public NetworkVariable<int> currenthealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        currenthealth.Value = maxhealth;
    }

    // Update is called once per frame


    [ServerRpc]
    public void TakedamageServerRpc(int damage)
    {
        if (!IsServer) return;
        currenthealth.Value -= damage;

        if (currenthealth.Value < 0)
        {
            NetworkObject netobj = GetComponent<NetworkObject>();
            if (netobj != null)
            {
                netobj.Despawn(true);
            }
        }
    }

    public void Takedamage(int damage)
    {
        if (!IsServer) return;
        currenthealth.Value -= damage;

        if (currenthealth.Value < 0)
        {
            NetworkObject netobj = GetComponent<NetworkObject>();
            if (netobj != null)
            {
                netobj.Despawn(true);
            }
        }
    }



}

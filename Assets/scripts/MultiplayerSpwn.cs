using Unity.Netcode;
using UnityEngine;
[DefaultExecutionOrder(0)]

public class MultiplayerSpwn : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();


        if (OwnerClientId == 0)
        {
            transform.position = GameObject.FindGameObjectWithTag("SpawnHost").transform.position;
        }
        if (OwnerClientId == 1)
        {
            transform.position = GameObject.FindGameObjectWithTag("SpawnClient").transform.position;
        }
    }
}

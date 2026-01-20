using Unity.Netcode;
using UnityEngine;

public class SpawnControllers : NetworkBehaviour
{
    public GameObject gunPrefab;
    public Transform spawnPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnNetworkSpawn()
    {
        // base.OnNetworkSpawn();
        {
            if (!IsServer) return;
            GameObject gun = Instantiate(gunPrefab, spawnPos.position, spawnPos.rotation);
            gun.GetComponent<NetworkObject>().Spawn();
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}

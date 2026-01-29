using System.Collections;
using Unity.Netcode;
using UnityEngine;
[DefaultExecutionOrder(0)]

public class MultiplayerSpwn : NetworkBehaviour
{
    Rigidbody rb;
    public static NetworkVariable<int> carsSpawned = new NetworkVariable<int>(0);
    public static NetworkVariable<bool> started = new NetworkVariable<bool>(false);

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (started.Value && rb.isKinematic)
        {
            rb.isKinematic = false;
        }
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            StartCoroutine(Spawn());

        }
        // StartCoroutine(Onphysics());


    }
    IEnumerator Spawn()
    {

        if (OwnerClientId == 0)
            transform.position = GameObject.FindGameObjectWithTag("SpawnHost").transform.position;

        if (OwnerClientId == 1)
            transform.position = GameObject.FindGameObjectWithTag("SpawnClient").transform.position;


        yield return new WaitForSeconds(5f);


        // rb.isKinematic = false;
        if (IsServer)
        {
            carsSpawned.Value++;
        }


    }



    // IEnumerator Onphysics()
    // {
    //     yield return new WaitForSeconds(5f);

    //     rb.isKinematic = false;
    // }
}

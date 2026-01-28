using System.Collections;
using Unity.Netcode;
using UnityEngine;
[DefaultExecutionOrder(0)]

public class MultiplayerSpwn : NetworkBehaviour
{
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            StartCoroutine(Spawn());

        }
        StartCoroutine(Onphysics());


    }
    IEnumerator Spawn()
    {

        if (OwnerClientId == 0)
            transform.position = GameObject.FindGameObjectWithTag("SpawnHost").transform.position;

        if (OwnerClientId == 1)
            transform.position = GameObject.FindGameObjectWithTag("SpawnClient").transform.position;


        yield return new WaitForSeconds(5f);


        rb.isKinematic = false;


    }



    IEnumerator Onphysics()
    {
        yield return new WaitForSeconds(5f);

        rb.isKinematic = false;
    }
}

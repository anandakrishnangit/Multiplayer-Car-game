using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Carguns : NetworkBehaviour
{

    public GameObject gun1;
    public Transform rayorgin;




    private bool gun1active = false;
    private float range = 70f;

    private RaycastHit hit;

    void Start()
    {
        gun1.SetActive(false);

    }

    void Update()
    {
        if (!IsOwner) return;

        if (gun1active && Input.GetKeyDown(KeyCode.F))
        {

            Gun1ShootServerRpc(rayorgin.position, rayorgin.forward);
        }

        Debug.DrawRay(rayorgin.position, rayorgin.forward * range, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if (other.CompareTag("gun1"))
        {
            ActivateGunClientRpc();
            NetworkObject netobj = other.GetComponentInParent<NetworkObject>();
            if (netobj != null)
            {
                netobj.Despawn();

                // Destroy(other.gameObject);
            }

        }
    }

    IEnumerator GunActivation()
    {
        gun1.SetActive(true);
        gun1active = true;

        yield return new WaitForSeconds(20f);

        gun1.SetActive(false);
        gun1active = false;
    }



    [ServerRpc]
    void Gun1ShootServerRpc(Vector3 origin, Vector3 direction)
    {
        ShootRayServer(origin, direction);
    }

    void ShootRayServer(Vector3 origin, Vector3 direction)
    {
        if (!IsServer) return;

        if (Physics.Raycast(origin, direction, out hit, range))
        {
            Debug.Log("Hit: " + hit.collider.name);


            NetworkObject netObj = hit.collider.GetComponentInParent<NetworkObject>();

            if (netObj != null)
            {
                netObj.Despawn();
            }
            // if (netObj != null)
            // {
            //     Carhealth health = GetComponent<Carhealth>();
            //     if (health != null)
            //     {
            //         health.Takedamage(10);
            //     }
            // }
        }
    }


    [ClientRpc]
    void ActivateGunClientRpc()
    {
        StartCoroutine(GunActivation());
    }
}

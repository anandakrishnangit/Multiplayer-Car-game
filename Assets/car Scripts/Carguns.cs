using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class Carguns : NetworkBehaviour
{
    [Header("Gun Settings")]
    public GameObject gun1;
    public Transform rayorgin;
    public Transform bulletspawn;
    public GameObject bulletprefab;

    private bool gun1active = false;
    private float range = 30f;

    private RaycastHit hit;

    void Start()
    {
        gun1.SetActive(false);
    }

    void Update()
    {

        if (!IsOwner) return;

        if (gun1active && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Gun1ShootServerRpc();
        }

        Debug.DrawRay(rayorgin.position, rayorgin.forward * range, Color.red);
    }


    void OnTriggerEnter(Collider other)
    {

        if (!IsServer) return;

        if (other.CompareTag("gun1"))
        {
            ActivateGunClientRpc();
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
    void Gun1ShootServerRpc()
    {

        GameObject bullet = Instantiate(bulletprefab, bulletspawn.position, bulletspawn.rotation);

        bullet.GetComponent<NetworkObject>().Spawn();

        ShootRayServer();
    }

    void ShootRayServer()
    {
        if (!IsServer) return;

        if (Physics.Raycast(rayorgin.position, rayorgin.forward, out hit, range))
        {
            if (hit.collider.CompareTag("enemy"))
            {
                NetworkObject netObj =
                    hit.collider.GetComponent<NetworkObject>();

                if (netObj != null)
                {
                    netObj.Despawn();
                }
            }
        }
    }


    [ClientRpc]
    void ActivateGunClientRpc()
    {
        StartCoroutine(GunActivation());
    }
}

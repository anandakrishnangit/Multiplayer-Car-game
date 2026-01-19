using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float speed = 40f;
    public float lifeTime = 3f;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
            Invoke(nameof(DestroyBullet), lifeTime);
        }
    }

    void DestroyBullet()
    {
        GetComponent<NetworkObject>().Despawn();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) return;

        if (collision.collider.CompareTag("enemy"))
        {
            NetworkObject netObj = collision.collider.GetComponent<NetworkObject>();
            if (netObj != null)
            {
                netObj.Despawn();
            }
        }

        GetComponent<NetworkObject>().Despawn();
    }
}

using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HitCube : NetworkBehaviour
{
    public TextMeshProUGUI laptext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NetworkVariable<int> m_HitCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> m_HostCount = new NetworkVariable<int>(0);
    public NetworkVariable<int> m_ClientCount = new NetworkVariable<int>(0);
    public int winHits = 2;




    void Update()
    {
        if (laptext == null || !IsSpawned) return;
        if (IsHost)
        {
            laptext.text = "Lap: " + m_HostCount.Value;
        }
        else if (IsClient)
        {
            laptext.text = "Lap: " + m_ClientCount.Value;
        }


    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        m_HitCount.OnValueChanged += OnHitValueChanged;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        m_HitCount.OnValueChanged -= OnHitValueChanged;
    }

    private void OnHitValueChanged(int oldValue, int newValue)
    {
        Debug.Log("Hit Count Updated : " + newValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Detect")
        {
            Debug.Log("hit");
            NetworkObject networkObject = other.GetComponent<NetworkObject>();
            if (IsClient && networkObject != null && networkObject.IsOwner)
            {
                AddHitServerRpc(networkObject.OwnerClientId);
            }
            CheckWin();
        }
    }
    void CheckWin()
    {
        if (m_HostCount.Value >= winHits)
        {
            GameResult.Instance.SetResultClientRpc("HOST WIN");
        }
        if (m_ClientCount.Value >= winHits)
        {
            GameResult.Instance.SetResultClientRpc("CLIENT WIN");
        }

    }

    [Rpc(SendTo.Server)]
    private void AddHitServerRpc(ulong playerId)
    {
        m_HitCount.Value++;
        if (playerId == 0)
        {
            m_HostCount.Value++;
        }
        else if (playerId == 1)
        {
            m_ClientCount.Value++;
        }
    }
}

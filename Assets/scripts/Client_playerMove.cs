using Unity.Cinemachine;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Client_playerMove : NetworkBehaviour
{
    [SerializeField] private CarController m_CarController;
    [SerializeField] private Transform m_cameraFollow;
    [SerializeField] private PlayerInput m_playerInput;

    void Awake()
    {
        m_CarController.enabled = false;
        m_playerInput.enabled = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsClient;


        if (!IsOwner)
        {
            enabled = false;
            m_CarController.enabled = false;         
            m_playerInput.enabled = false;
            return;
        }

        m_CarController.enabled = true;       
        m_playerInput.enabled = true;


        m_cameraFollow = GameObject.FindGameObjectWithTag("PlayerCamera").transform;
        m_cameraFollow.GetComponent<CinemachineCamera>().Follow = gameObject.transform;
      // m_cameraFollow.GetComponent<CinemachineCamera>().LookAt = gameObject.transform;




    }
}

using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameResult : NetworkBehaviour
{
    public static GameResult Instance;

    public GameObject panel;
    public TextMeshProUGUI resultText;

    void Awake()
    {
        Instance = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ClientRpc]
    public void SetResultClientRpc(string result)
    {
        panel.SetActive(true);

        if (IsHost && result == "HOST WIN")
            resultText.text = "YOU WIN";
        else if (!IsHost && result == "CLIENT WIN")
            resultText.text = "YOU WIN";
        else
            resultText.text = "YOU LOSE";
    }
}

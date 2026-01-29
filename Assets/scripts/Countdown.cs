using System.Collections;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class Countdown : NetworkBehaviour
{
    public TMP_Text countdown;
    bool started = false;
    public GameObject plane;
    bool isplane = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!started && MultiplayerSpwn.carsSpawned.Value >= 2)
        {
            started = true;

            if (IsServer)
                StartCoroutine(CountdownCo());
        }
        if (IsServer && isplane == false)
        {
            PlaneClientRpc();

            isplane = true;
        }

    }
    IEnumerator CountdownCo()
    {
        for (int i = 3; i > 0; i--)
        {
            TextClientRpc(i.ToString());
            yield return new WaitForSeconds(1f);
        }

        TextClientRpc("GO");
        MultiplayerSpwn.started.Value = true;
        yield return new WaitForSeconds(2f);
        TextClientRpc("");
        isplane = false;
    }


    [ClientRpc]
    void TextClientRpc(string text)
    {
        countdown.text = text;
    }
    [ClientRpc]
    void PlaneClientRpc()
    {


        plane.SetActive(false);

    }

}

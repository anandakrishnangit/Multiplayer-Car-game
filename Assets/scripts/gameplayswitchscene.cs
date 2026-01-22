using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Audio;

public class gameplayswitchscene : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clicksound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private System.Collections.IEnumerator play()
    {
        audioSource.PlayOneShot(clicksound);
        yield return new WaitForSeconds(clicksound.length);
        SceneManager.LoadScene(1);

    }
    public void playgame()
    {
        StartCoroutine(play());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}

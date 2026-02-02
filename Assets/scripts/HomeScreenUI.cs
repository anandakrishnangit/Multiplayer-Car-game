using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenUI :NetworkBehaviour
{
   public void PlayButton()
   {
       SceneManager.LoadScene(2);
   }

    public void QuitToHome()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitFromApplicaion()
    {
        Application.Quit();
    }
    public void ToggleAudio()
    {
        AudioListener.pause = !AudioListener.pause;
    }
    public void SettingsUI()
    {
        SceneManager.LoadScene(3);
    }
}

using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScreenUI :NetworkBehaviour
{
   public void PlayButton()
   {
       SceneManager.LoadScene(2);
   }
}

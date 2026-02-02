using UnityEngine;

public class MouseLock : MonoBehaviour
{
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnlockCursor();
        }
        if (Input.GetMouseButtonDown(1))
        {
            LockCursor();
        }
    }



    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;                   
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;   
        Cursor.visible = true;                   
    }
}

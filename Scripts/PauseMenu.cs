using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
   private void Update()
   {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
           if (GameIsPaused)
           {
               Resume();
           }
           else
           {
               Pause();
           }
       }
   }
       

   public void Resume()
   {
       Cursor.lockState = CursorLockMode.Confined;
       Cursor.visible = false;
       pauseMenuUI.SetActive(false);
       Time.timeScale = 1f;
       GameIsPaused = false;
   }

   void Pause()
   { 
       Cursor.lockState = CursorLockMode.Confined; 
       Cursor.visible = true;
       pauseMenuUI.SetActive(true);
       Time.timeScale = 0f;
       GameIsPaused = true;
   }

   public void QuitToMenu()
   {
       SceneManager.LoadScene(0);
   }

   public void Quit()
   {
    Application.Quit();   
   }
}

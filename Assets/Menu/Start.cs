using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Start : MonoBehaviour
{
    public void startGame() {  
        SceneManager.UnloadSceneAsync("StartMenu");  
        SceneManager.LoadScene("Game");  
    }

    public void setFullscreen()
    {
        Screen.SetResolution(1920, 1080, !Screen.fullScreen);
    }
}

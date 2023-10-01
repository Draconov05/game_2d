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
        Screen.SetResolution(640, 275, !Screen.fullScreen);
    }
}

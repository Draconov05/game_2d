using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Exit : MonoBehaviour
{
    public void endGame() {  
        SceneManager.LoadSceneAsync("GameOver"); 
        SceneManager.UnloadSceneAsync("Game");  
    }

    public void reStart() {  
        SceneManager.UnloadSceneAsync("GameOver"); 
        SceneManager.LoadSceneAsync("Game");  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void LoadSceneByName(string sceneName){
        if(!string.IsNullOrEmpty(sceneName)){
            SceneManager.LoadScene(sceneName);
        }
    }
    
}

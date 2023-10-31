using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Session session;

    void Awake()
    {
        session = FindObjectOfType<Session>();
    }

    public void LoadSceneByName(string scene){
        if (!string.IsNullOrEmpty(scene))
        {
            if (scene == "WrestlerSelectionPVC")
            {
                session.category = Category.PvC;
                session.gamemode = Gamemode.OvO;
            }
            if (scene == "WrestlerSelectionPVP")
            {
                session.category = Category.PvP;
                session.gamemode = Gamemode.OvO;
            }
            SceneManager.LoadScene(scene);
        }
    }
    
}

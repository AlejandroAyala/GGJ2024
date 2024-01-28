using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLoader : MonoBehaviour
{
    //public Animator transition;
    // Update is called once per frame

    public void LoadNextLevel(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }   

}

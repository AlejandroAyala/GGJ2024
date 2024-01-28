using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    // Update is called once per frame

    public void LoadNextLevel(int SceneID)
    {
        StartCoroutine(LoadLevel(SceneID));
    }   

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(1);
        //LoadScene
        SceneManager.LoadScene(levelIndex);
    }
}

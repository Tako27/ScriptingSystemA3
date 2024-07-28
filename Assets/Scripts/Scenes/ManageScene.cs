using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

// Code Done By: Lee Ying Jie
// ================================
// This script handles management of scenes in the game
public class ManageScene : MonoBehaviour
{
    public void OpenScene(string sceneName, UnityAction actionOnLoad) //open scene 
        {
            //add scene asynchronously
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (asyncOp) =>
            {
                //run actionOnLoad after scene finishes loading
                actionOnLoad?.Invoke();
            };
        }

    public void CloseScene (string sceneName) //close scene
    {
        //find scene by name
        Scene toClose = SceneManager.GetSceneByName(sceneName);
        if (toClose.IsValid())
        {
            //unload if valid scene found
            SceneManager.UnloadSceneAsync(toClose);
        }
    }
}

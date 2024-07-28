using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ManageScene : MonoBehaviour
{
    public void OpenScene(string sceneName, UnityAction actionOnLoad)
        {
            //add scene asynchronously
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (asyncOp) =>
            {
                //run actionOnLoad after scene finishes loading
                actionOnLoad?.Invoke();
            };
        }

        public void CloseScene (string sceneName)
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

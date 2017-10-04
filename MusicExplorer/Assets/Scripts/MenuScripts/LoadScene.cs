using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        
#if UNITY_EDITOR //if scene is opened in editor;
        UnityEditor.EditorApplication.isPlaying = false;
#else //if scene is build
        Application.Quit();
#endif
    }
}

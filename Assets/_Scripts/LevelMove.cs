using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove : MonoBehaviour
{
    //public string sceneName;
    public int sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }
    public void LoadScene(int index)
    {
        Debug.Log("LoadScene called with: " + index.ToString());
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }
}

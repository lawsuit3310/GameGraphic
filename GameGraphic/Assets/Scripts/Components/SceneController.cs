using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static GameObject Instance; 
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    public static void LoadScene(string name)
    {
        LoadingSceneManager.TargetSceneName = name;
        LoadingSceneManager.CurrentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LoadingScene");
    }
}

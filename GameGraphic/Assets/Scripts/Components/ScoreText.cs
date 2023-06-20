using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var t = GetComponent<TMP_Text>();
        t.text = GameManager.score + "";
    }

    public void Retry(string name)
    {
        SceneController.LoadScene(name);
    }
}

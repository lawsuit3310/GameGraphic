using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static  TMP_Text ScoreText;
    public static int score = 0;
    public Player player;

    // Start is called before the first frame update
    private void Awake()
    {
        ScoreText = GameObject.Find("ScoreText").GetComponent<TMP_Text>();
    }

    void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        ScoreText.text = score + "";
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        RenewalScoreText();
    }

    void FixedUpdate()
    {
        RenewalScoreText();
    }

    public void RenewalScoreText()
    {
        scoreText.text = GameManager.score + "";
    }
}

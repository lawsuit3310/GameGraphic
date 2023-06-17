using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text[] itemText;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Awake()
    {
        RenewalScoreText();
    }

    void FixedUpdate()
    {
        RenewalScoreText();
        RenewalItemCount();
    }

    public void RenewalScoreText()
    {
        scoreText.text = GameManager.score + "";
    }

    public void RenewalItemCount()
    {
        // item 3 activate
        if (GameManager.Instance.player.isJumpUsed)
            itemText[0].text = "¡¿ 1";
        else
            itemText[0].text = "¡¿ 0";

        // item 2 activate
        if (GameManager.Instance.player.speedBoostTimer > 0)
            itemText[1].text = GameManager.Instance.player.speedBoostDuration - (int)GameManager.Instance.player.speedBoostTimer + "s";
        else
            itemText[1].text = 0 + "s";

        // item 1 activate -> BlackHand.cs
        if (BlackHand.reduceDifficultyTimer > 0)
            itemText[2].text = BlackHand.reduceDifficultyDuration - (int)BlackHand.reduceDifficultyTimer + "s";
        else
            itemText[2].text = 0 + "s";
    }
}

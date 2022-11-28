﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public int score = -1;
    public Text playerScore;

    public Text playerHeath;

    public PlayerHealthAndWeapons player;

    void Start()
    {
        playerScore.text = "Score - " + score;
        playerHeath.text = "Heath - " + player.healthSys_.GetHealth();
    }

   void Update()
    {
        playerScore.text = "Score - " + score;
        playerHeath.text = "Heath - " + player.healthSys_.GetHealth();
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "nextLevel")
        {
            player.lastPlatformReachedY = collision.gameObject.transform.position.y;
            score += 1;
            collision.gameObject.tag = "Untagged";

        }
    }
}

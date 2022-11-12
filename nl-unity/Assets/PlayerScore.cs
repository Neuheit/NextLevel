using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;
    public Text playerScore;

    void Start()
    {
        playerScore.text = "Score - " + score;
    }

   void Update()
    {
        playerScore.text = "Score - " + score;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "nextLevel")
        {
            score += 1;
            collision.gameObject.tag = "Untagged";

        }
    }
}

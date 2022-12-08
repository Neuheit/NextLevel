using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAndWeapons : MonoBehaviour
{
    public HealthSystem healthSys_;

    public PlayerScore score;
    // Start is called before the first frame update
    void Awake()
    {
        healthSys_ = new HealthSystem(100,100);
        score = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(healthSys_.GetHealth() == 0){
            this.gameObject.SetActive(false);
            score.score += 1000;
        }
    }
}

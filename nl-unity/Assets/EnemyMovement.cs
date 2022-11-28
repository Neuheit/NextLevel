using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent nav;

    public EnemyHealthAndWeapons health;

    public PlayerHealthAndWeapons player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.player.transform.position;
        if(player.healthSys_.GetHealth() > 0)
            nav.SetDestination(new Vector3(pos.x - 2, pos.y, pos.z - 2));
    }
}

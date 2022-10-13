using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyChase : MonoBehaviour
{

    NavMeshAgent enemy;
    public GameObject Target;

    void Start()
    {
        Target = GameObject.FindWithTag("Player");
        enemy = GetComponent<NavMeshAgent>();


    }

    void Update()
    {
        enemy.SetDestination(Target.transform.position);        
    }
}

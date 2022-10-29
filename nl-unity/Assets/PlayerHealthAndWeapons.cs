using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthAndWeapons : MonoBehaviour
{
    public HealthSystem healthSys_;
    public Weapon meleeAttack_;
    private float attackInterval_;
    private float timeBetweenAttacks_;
    private void Awake()
    {
        healthSys_       = new HealthSystem(100,100);
        meleeAttack_     = new Weapon(20);
        attackInterval_  = 0;
        timeBetweenAttacks_ = 0.5f;
    }

    private void LateUpdate(){

    }
    private void OnTriggerStay(Collider other){
        //dealing damage to ememies
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("here");
            if(Time.time > attackInterval_ && Input.GetAxis("Fire1") != 0){
                other.GetComponent<EnemyHealthAndWeapons>().healthSys_.ReduceHealth(meleeAttack_.DamageAmount());
                attackInterval_ = Time.time + timeBetweenAttacks_;
            }
        }
    }
}

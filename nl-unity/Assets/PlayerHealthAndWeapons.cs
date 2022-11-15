using System;
using UnityEngine;

public class PlayerHealthAndWeapons : MonoBehaviour
{
    public AudioSource AttackAudio;
    public AudioSource DeathAudio;
    public HealthSystem healthSys_;
    public GameObject player;
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

    private void Update()
    {
        if(healthSys_.GetHealth() <= 0)
        {
            var collider = player.GetComponent<CapsuleCollider>();

            if(!collider.isTrigger)
            {
                collider.isTrigger = true;
                DeathAudio.Play();
            }
            //death logic here
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //dealing damage to ememies
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(Time.time > attackInterval_ && Input.GetAxis("Fire1") != 0){
                AttackAudio.Play();
                other.GetComponent<EnemyHealthAndWeapons>().healthSys_.ReduceHealth(meleeAttack_.DamageAmount());
                attackInterval_ = Time.time + timeBetweenAttacks_;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            
        }
    }

    public void TakeDamage(int damage)
    {
        healthSys_.ReduceHealth (damage);
    }
}

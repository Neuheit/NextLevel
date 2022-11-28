using UnityEngine;
using System;

public class EnemyAttack : MonoBehaviour
{

    public AudioSource HitAudio;
    public AudioSource RoarAudio;

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealthAndWeapons playerHealth;
    EnemyHealthAndWeapons enemyHealth;
    bool playerInRange;
    float timer;

    DateTimeOffset? timeUntilYellow;
    bool isYellow;
    DateTimeOffset? timeUntilRed;
    bool isRed;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent<PlayerHealthAndWeapons>();
        //playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealthAndWeapons>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            timeUntilYellow = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(1);
            timeUntilRed = DateTimeOffset.UtcNow + TimeSpan.FromSeconds(2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //dealing damage to ememies
        if(other.gameObject == player)
        {
            //Debug.Log("here1");

            var renderer = GetComponent<MeshRenderer>();

            if(timeUntilYellow != null && timeUntilYellow <= DateTimeOffset.UtcNow && !isYellow)
            {
                isYellow = true;
                renderer.material.color = Color.yellow;
                RoarAudio.Play();
            }

            if(timeUntilRed != null && timeUntilRed <= DateTimeOffset.UtcNow && isYellow)
            {
                isRed = true;
                renderer.material.color = Color.red;
            }

            /*
            if(Time.time > attackInterval_ && Input.GetAxis("Fire1") != 0){
                other.GetComponent<EnemyHealthAndWeapons>().healthSys_.ReduceHealth(meleeAttack_.DamageAmount());
                attackInterval_ = Time.time + timeBetweenAttacks_;
            }
            */
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
            isYellow = false;
            isRed = false;
            HitAudio.Stop();
            RoarAudio.Stop();
            var renderer = GetComponent<MeshRenderer>();
            renderer.material.color = Color.white;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.healthSys_.GetHealth() > 0 && isRed)
        {
            Attack ();
        }

        /*
        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
        */
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.healthSys_.GetHealth() > 0)
        {
            playerHealth.TakeDamage(attackDamage);
            HitAudio.Play();
        }
    }
}

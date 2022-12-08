using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthAndWeapons : MonoBehaviour
{
    public AudioSource DeathAudio;
    public HealthSystem healthSys_;
    public GameObject player;
    public float HealthBuff = 10.0f;
    private float attackInterval_;
    private float timeBetweenAttacks_;

    public float lastPlatformReachedY;

    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] public Text gameOverText;

    private void Awake()
    {
        healthSys_       = new HealthSystem(100,100);
        gameOverPanel.SetActive(false);
        gameOverText.gameObject.SetActive(false);
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
        }

        if(player.transform.position.y <= lastPlatformReachedY - 100)
        {
            //insert death logic
            gameOverPanel.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     //dealing damage to ememies
    //     if(other.gameObject.CompareTag("Enemy"))
    //     {
    //         if(Time.time > attackInterval_ && Input.GetAxis("Fire1") != 0){
    //             AttackAudio.Play();
    //             other.GetComponent<EnemyHealthAndWeapons>().healthSys_.ReduceHealth(meleeAttack_.DamageAmount());
    //             attackInterval_ = Time.time + timeBetweenAttacks_;
    //         }
    //     }
    // }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("BuffBox"))
        {
            healthSys_.AddHealth(HealthBuff);
            Destroy(other.gameObject);
        }
        if(other.gameObject.CompareTag("BuffBox+"))
        {
            healthSys_.AddHealth(HealthBuff);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        healthSys_.ReduceHealth (damage);
    }
}

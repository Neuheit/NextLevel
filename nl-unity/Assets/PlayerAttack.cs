using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private Collider enemy;
    private float timer;
    private bool enemyInRange;
    private bool attacking;

    public AudioSource AttackAudio;
    public Weapon meleeAttack_ = new Weapon(20);
    public float timeBetweenAttacks = 0.3f;
    void Start()
    {   
        anim         = GetComponent<Animator>();
        enemy        = null;
        timer        = 0f;
        enemyInRange = false;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // if(timer >= 5f){
        //     anim.SetTrigger("Attack");
        //     timer = 0f;
        // }
        if(timer >= timeBetweenAttacks && enemyInRange){
            Attack();
        }else if(enemyInRange){
            enemyInRange = false;
            enemy = null;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            anim.SetTrigger("Attack");
            attacking = true;
        }
    }
    void OnTriggerStay(Collider other){
       //dealing damage to ememies
        if(other.gameObject.CompareTag("Enemy") && attacking)
        {
            //anim.SetTrigger("Attack");
            enemyInRange = true;
            enemy = other;
            attacking = false;
        }
    }
    void Attack(){
        timer = 0f;
        var enemyHealth = enemy.GetComponent<EnemyHealthAndWeapons>().healthSys_;
        if(enemyHealth.GetHealth() > 0 ){
            enemyHealth.ReduceHealth(meleeAttack_.DamageAmount());
            AttackAudio.Play();
        }
       
    }
}

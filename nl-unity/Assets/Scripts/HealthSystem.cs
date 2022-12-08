using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private float maxHealth_;
    private float maxStamina_;
    private float health_;
    private float stamina_;
    public HealthSystem(float health, float stamina, float maxHealth = 100f, float maxStamina = 100f){
        maxHealth_ = maxHealth;
        maxStamina_ = maxStamina;
        health_ = health;
        stamina_ = stamina;
    }
    public void AddHealth(float amount){
        health_ += Mathf.Abs(amount);
        if(health_ > maxHealth_){
            health_ = maxHealth_;
        }
    }
    public void AddStamina(float amount){
        stamina_ += Mathf.Abs(amount);
        if(stamina_ < maxStamina_){
            stamina_ = maxStamina_;
        }
    }
    public void ReduceHealth(float amount){
        health_ -= Mathf.Abs(amount);
        if(health_ < 0f){
            health_ = 0f;
        }
        //Debug.Log(health_);
    }
    public void ReduceStamina(float amount){
        stamina_ -= Mathf.Abs(amount);
        if(stamina_ < 0){
            stamina_ = 0;
        }
    }
    public float GetHealth(){
        return health_;
    }
    public float GetStamina(){
        return stamina_;
    }
}

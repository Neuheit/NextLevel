using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private float damage_;
    public Weapon(float damage){
        damage_ = Mathf.Abs(damage);
    }
    public float DamageAmount(){
        return damage_;
    }
}

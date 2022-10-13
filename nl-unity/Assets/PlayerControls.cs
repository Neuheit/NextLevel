using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HealthSystem
{
    private const int initialHealth = 1000;
    public int CurrentHealth;
    public int Stamina;
    HealthSystem()
    {
        CurrentHealth = initialHealth;
        Stamina = 100;
    }
    int HealthPercentage()
    {
        return CurrentHealth / initialHealth;
    }




}
public class PlayerControls : MonoBehaviour
{
    public Collider platformPrefab;
    void Start()
    {
        for(int i = 0; i < 4; ++i)
        {
            Collider newPlatform = Instantiate(platformPrefab, transform.position + new Vector3(0, 10.0f*(i+1), 0.0f * i), transform.rotation) as Collider;
        }
    }

    void Update()
    {
    }
}

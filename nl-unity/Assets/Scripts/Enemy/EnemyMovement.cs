using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public EnemyHealthAndWeapons health;
    public float currentSpeed      = 5f;
    public float maxSpeed          = 9f;
    public float incrementPerLevel = 0.3f;

    private PlayerHealthAndWeapons player;
    private Vector3 levelSize = new Vector3(28, 0, 28);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthAndWeapons>();
    }
    // Update is called once per frame
    void Update()
    {
        var pos = player.player.transform.position;
        if(player.healthSys_.GetHealth() > 0 && DistanceY(player.transform.position.y, transform.position.y) < 15){
            var target = new Vector3(pos.x - 2, transform.position.y, pos.z - 2);
            var newPos = Vector3.MoveTowards(transform.position, target, currentSpeed * Time.deltaTime);
            var levelCenter = transform.parent.transform.position;
            if( newPos.x < levelCenter.x + levelSize.x/2.0f  && newPos.x > levelCenter.x - levelSize.x/ 2.0f
                && newPos.z < levelCenter.z + levelSize.z/2.0f  && newPos.z > levelCenter.z - levelSize.z/ 2.0f){
                    transform.position = newPos;
                }
        }
    }
    public void updateSpeedByLevel(int level){
        float newSpeed = currentSpeed + level * incrementPerLevel;
        if(newSpeed < maxSpeed){
            currentSpeed = newSpeed; 
        }else if( currentSpeed != maxSpeed){
            currentSpeed = maxSpeed;
        }
    }

    private float DistanceY(float y1, float y2){
        return Mathf.Abs(y1 - y2);
    }
}

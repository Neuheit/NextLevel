using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLevel : MonoBehaviour
{
    private Vector3 maxOffset;
    private Vector3 minOffset;
    private Vector3 minTarget;
    private Vector3 maxTarget;
    private Vector3 currTarget;
    private Transform player;
    private bool movePlayer = false;
    private float   speed;
    public float    minSpeed = 5;
    public float    maxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        var generator  = GameObject.FindWithTag("LevelGenerator");
        maxOffset      = generator.GetComponent<LevelGenerator>().maxOffset;
        minOffset      = generator.GetComponent<LevelGenerator>().minOffset;
        speed          = Random.Range(minSpeed, maxSpeed);
        player         = GameObject.FindWithTag("Player").transform;
        SetTargets();
    }   

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move(){
        var newPos = Vector3.MoveTowards(transform.position, currTarget, speed * Time.deltaTime);
        Vector3 increment = newPos - transform.position;

        MovePlayer(increment);
        transform.position = newPos;

        // Check if the position of the level and the target is approximately equal
        if (Vector3.Distance(transform.position, currTarget) < 0.01f)
        {
            //toggling between min and maxTarget
            currTarget = (currTarget == maxTarget) ? minTarget: maxTarget;
        }
    }
    void MovePlayer(Vector3 increment){
        if(movePlayer){
            player.position += increment;
            movePlayer = false;
        }
    }
    void OnCollisionStay(Collision other){
        if(other.gameObject.CompareTag("Player")){
            //make sure the player if fully above the level.
                movePlayer = true;
        }
    }
    void SetTargets(){
        //randomly choose a axis to move along in x, y, z, or a combinaiton xy, zx, zy
        minTarget = maxTarget = transform.position;
        switch (Random.Range(0, 5)){
            case 0://x
                minTarget.x += minOffset.x;
                maxTarget.x += maxOffset.x;
                break;
            case 1://y
                minTarget.y -= minOffset.y;
                maxTarget.y += maxOffset.y;
                break;
            case 2://z
                minTarget.z += minOffset.z;
                maxTarget.z += maxOffset.z;
                break;
            case 3://zx
                minTarget.x += minOffset.x; minTarget.z += minOffset.z;
                maxTarget.x += maxOffset.x; maxTarget.z += maxTarget.z;
                break;
            case 4://yz
                minTarget.y -= minOffset.y; minTarget.z += minOffset.z;
                maxTarget.y += maxOffset.y; maxTarget.z += maxTarget.z;
                break;
            case 5://yx
                minTarget.y -= minOffset.y; minTarget.x += minOffset.x;
                maxTarget.y += maxOffset.y; maxTarget.x += maxTarget.x;
                break;
        }
        currTarget = minTarget;
    }


}

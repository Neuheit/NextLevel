using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //[SerializeField] makes the private variables visible in the inspector
    [SerializeField] private Transform initialLevel;
    [SerializeField] private Transform level;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 maxOffset = new Vector3( 40, 20,  40);
    [SerializeField] private Vector3 minOffset = new Vector3(-40, 10, -40);
    private Vector3 lastSpawnPosition;

    private Queue<Transform> levels = new Queue<Transform>();
    private void Awake() {
        levels.Enqueue(initialLevel);
        lastSpawnPosition = initialLevel.position;
        int initialSpawns = 3;
        for(int i = 0; i < initialSpawns; ++i){
            SpawnLevel();
        }
    }
    private void Update(){
        //if player position y is within a minimun of 3 platforms from the highest platform
        if(DistanceY(player.position.y, lastSpawnPosition.y) < maxOffset.y * 2){
            SpawnLevel();
            Debug.Log(DistanceY(player.position.y, levels.Peek().position.y));
            while(DistanceY(player.position.y, levels.Peek().position.y) > maxOffset.y * 3){
                Destroy(levels.Dequeue().gameObject);
            }
        }
    }
    private void SpawnLevel(){
        Vector3 offset = new Vector3(
                            Random.Range(minOffset.x, maxOffset.x),
                            Random.Range(minOffset.y, maxOffset.y),
                            Random.Range(minOffset.z, maxOffset.z)
                        );
        lastSpawnPosition = lastSpawnPosition + offset;
        Transform newLevel = Instantiate(level, lastSpawnPosition, Quaternion.identity) as Transform;
        levels.Enqueue(newLevel);
    }

    private float DistanceY(float y1, float y2){
        return Mathf.Abs(y1 - y2);
    }
}

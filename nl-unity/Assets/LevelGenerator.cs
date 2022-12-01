using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //[SerializeField] makes the private variables visible in the inspector
    [SerializeField] private Transform initialLevel;
    [SerializeField] private Transform level;
    [SerializeField] private Transform player;
    [SerializeField] private Transform enemyPrefab;
    public  Vector3 maxOffset = new Vector3( 20, 20,  20);
    public  Vector3 minOffset = new Vector3(-20, 20, -20);

    private Vector3 levelSize = new Vector3(28, 0, 28);
    private Vector3 lastSpawnPosition;

    private Queue<Transform> levels = new Queue<Transform>();
    private void Awake() {
        levels.Enqueue(initialLevel);
        lastSpawnPosition = initialLevel.position;
        int initialSpawns = 3;
        for(int i = 0; i < initialSpawns; ++i){
            SpawnLevel();
        }
        
        var buffs = GameObject.FindGameObjectsWithTag("BuffBox");
        foreach(var b in buffs)
        {
            b.GetComponent<MeshRenderer>().enabled = false;
            b.GetComponent<BoxCollider>().enabled = false;
        }

        var buffsP = GameObject.FindGameObjectsWithTag("BuffBox+");
        foreach(var b in buffsP)
        {
            b.GetComponent<MeshRenderer>().enabled = false;
            b.GetComponent<BoxCollider>().enabled = false;
        }

        var dBuffs = GameObject.FindGameObjectsWithTag("DeBuff");
        foreach(var b in dBuffs)
        {
            b.GetComponent<MeshRenderer>().enabled = false;
            b.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void Update(){
        //if player position y is within a minimun of 3 platforms from the highest platform
        if(DistanceY(player.position.y, lastSpawnPosition.y) < maxOffset.y * 2){
            SpawnLevel();
            while(DistanceY(player.position.y, levels.Peek().position.y) > maxOffset.y * 3){
                Transform curr = levels.Dequeue();
                if(curr == initialLevel){
                    curr.gameObject.SetActive(false);
                }else{
                    Destroy(curr.gameObject);
                }
            }
        }
    }
    private void SpawnLevel(){
        Vector3 offset = new Vector3(
                            Random.Range(minOffset.x, maxOffset.x),
                            Random.Range(minOffset.y, maxOffset.y),
                            Random.Range(minOffset.z, maxOffset.z)
                        );
        lastSpawnPosition = new Vector3(initialLevel.position.x, lastSpawnPosition.y, initialLevel.position.z) + offset;
        Transform newLevel = Instantiate(level, lastSpawnPosition, Quaternion.identity) as Transform;
        AddBuffs(newLevel);
        AddEnemies(newLevel);
        int rand = Random.Range(0, 5);
        if(rand == 0){//20% percent chance
            newLevel.gameObject.AddComponent<MoveLevel>();
        }
        levels.Enqueue(newLevel);
    }

    private void AddBuffs(Transform level)
    {
        var buffs = GameObject.FindGameObjectsWithTag("BuffBox").Where(x => x.transform.position.y > level.position.y).ToArray();
        var buffsP = GameObject.FindGameObjectsWithTag("BuffBox+").Where(x => x.transform.position.y > level.position.y).ToArray();
        var dBuffs = GameObject.FindGameObjectsWithTag("DeBuff").Where(x => x.transform.position.y > level.position.y).ToArray();

        int rand = Random.Range(0, 4);
        if(rand == 0) //33% to spawn buffs
        {
            rand = Random.Range(0, buffs.Length - 1);
            var buff = buffs[rand];
            buff.GetComponent<MeshRenderer>().enabled = true;
            buff.GetComponent<BoxCollider>().enabled = true;
        }

        rand = Random.Range(0, 4);
        if(rand == 0) //33% to spawn buffs
        {
            foreach(var d in dBuffs)
            {
                d.GetComponent<MeshRenderer>().enabled = true;
                d.GetComponent<BoxCollider>().enabled = true;
            }
        }

        rand = Random.Range(0, 11);
        if(rand == 0) //10% to spawn buff+
        {
            rand = Random.Range(0, buffsP.Length - 1);
            var buff = buffs[rand];
            buff.GetComponent<MeshRenderer>().enabled = true;
            buff.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void AddEnemies(Transform level)
    {
        int num = Random.Range(0,3);
        level.gameObject.SetActive(true);
        Vector3 center = level.Find("LevelCenter").position;
        for(int i = 0; i < num; ++i){
            Vector3 newPos = new Vector3(
                            center.x + Random.Range(-levelSize.x/2.0f, levelSize.x/2.0f),
                            center.y + enemyPrefab.localScale.y,
                            center.z + Random.Range(-levelSize.z/2.0f, levelSize.z/2.0f)
                            );
            var newEnemy = Instantiate(enemyPrefab, newPos, Quaternion.identity ) as Transform;
            newEnemy.parent = level;
        }
    }


    private float DistanceY(float y1, float y2){
        return Mathf.Abs(y1 - y2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToSpawn = new List<GameObject>();
    [SerializeField] int amountToSpawn = 1;
    [SerializeField] Vector2 randomScale = new Vector2(1,1);
    [SerializeField] float timeBetweenSpawns;
    float timeSinceLastSpawn;

    bool playerDead = false;

    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    private void Update() {
        if(playerDead){return;}
        if(timeSinceLastSpawn > timeBetweenSpawns){
            timeSinceLastSpawn = 0;
            SpawnThing();
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void SpawnThing(){
        for(int i = 0; i < amountToSpawn; i++){
            int randomIndex = Random.Range(0,objectsToSpawn.Count - 1);
            Vector3 pos = new Vector3(Random.Range(-80.0f,-90.0f),Random.Range(3.0f,5.0f),Random.Range(-6.0f,6.0f));
            GameObject obj = Instantiate(objectsToSpawn[randomIndex],pos,Quaternion.identity);
            float newScale = Random.Range(randomScale.x, randomScale.y);
            obj.transform.localScale = new Vector3(newScale,newScale,newScale);
            
            if(obj.TryGetComponent(out ItemMovement im)){
                im.independentSpeed = true;
                im.movementSpeed = Random.Range(4.0f,15.0f);
            }
            if(obj.TryGetComponent(out ItemScript itemScript)){
                itemScript.independentSpeed = true;
                itemScript.movementSpeed = Random.Range(4.0f,15.0f);
            }
        }
    }
}

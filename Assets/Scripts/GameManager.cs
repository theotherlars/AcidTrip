using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float trippiness = 0;
    [SerializeField] List<GameObject> MushRooms = new List<GameObject>(); 
    [SerializeField] float timeBetweenObstacleSpawns = 1.5f;
    [SerializeField] float timeBetweenCollectableSpawns = 3f;
    [SerializeField] float transitionTime = 2;
    [SerializeField] List<TripState> tripStates = new List<TripState>();
    TripState currentTripState;
    TripState tripStateLastFrame = null;
    float timeSinceLastObstacleSpawn;
    float timeSinceLastCollectableSpawn;
    [SerializeField] float timeBetweenScenarySpawn;
    float timeSinceLastScenarySpawn;
    GroundMovement groundMovement;
    BendControllerRadial bendController;
    ScoreManager scoreManager;

    bool playerDead = false;

    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
        bendController = FindObjectOfType<BendControllerRadial>();
        groundMovement = FindObjectOfType<GroundMovement>();
        scoreManager = FindObjectOfType<Canvas>().GetComponent<ScoreManager>();
        currentTripState = tripStates[0];
        bendController.HorizonWaves = true;
    }
    
    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    private void Update() {
        if(playerDead){return;}
        trippiness = (float)scoreManager.score / 100;
        SetTrippiness(trippiness);
        UpdateTripValues();

        if(timeSinceLastObstacleSpawn > timeBetweenObstacleSpawns){
            timeSinceLastObstacleSpawn = 0;
            if(Random.value > 0.5f){
                SpawnObstacles();
            }
            else{
                SpawnObstacles();
                SpawnObstacles();
            }
        }
        if(timeSinceLastCollectableSpawn > timeBetweenCollectableSpawns){
            timeSinceLastCollectableSpawn = 0;
            SpawnCollectables();
        }

        if(timeSinceLastScenarySpawn > timeBetweenScenarySpawn){
            timeSinceLastScenarySpawn = 0;
            SpawnScenary();
        }

        timeSinceLastCollectableSpawn += Time.deltaTime;
        timeSinceLastObstacleSpawn += Time.deltaTime;
        timeSinceLastScenarySpawn += Time.deltaTime;
    }

    private void SpawnScenary(){
        int randomIndex = Random.Range(0,currentTripState.Obstacles.Count);
        Vector3 pos = new Vector3(Random.Range(-90.0f,-100.0f),0f,0.0f);
        if(Random.value > 0.5f){
            pos.z = Random.Range(-10.0f, -40.0f);
        }
        else{
            pos.z = Random.Range(10.0f, 40.0f);
        }
        Instantiate(currentTripState.Obstacles[randomIndex],pos,Quaternion.identity);
    }

    private void SetTrippiness(float newTrippiness){
        

        if(newTrippiness <= 0.1){
            currentTripState = tripStates[0];
        }
        else if(newTrippiness > 0.1 && newTrippiness < 0.2){
            currentTripState = tripStates[1];
        }
        else if(newTrippiness > 0.2 && newTrippiness <= 0.5){
            currentTripState = tripStates[2];
        }
        else if(newTrippiness > 0.5 && newTrippiness <= 0.6){
            currentTripState = tripStates[3];
        }
        else if(newTrippiness > 0.6 && newTrippiness <= 0.7){
            currentTripState = tripStates[4];
        }
        else if(newTrippiness > 0.7 && newTrippiness <= 0.8){
            currentTripState = tripStates[5];
        }
        else if(newTrippiness > 0.8 && newTrippiness <= 0.9){
            currentTripState = tripStates[6];
        }
        else if(newTrippiness > 0.9 && newTrippiness <= 1){
            currentTripState = tripStates[7];
        }
    }

    private void UpdateTripValues(){
        bendController.Curvature = Mathf.Lerp(bendController.Curvature, currentTripState.Curvature, Time.deltaTime * transitionTime);
        bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, currentTripState.HorizonWaveFrequency, Time.deltaTime * transitionTime);
        groundMovement.movementSpeed = currentTripState.MovementSpeed;
        timeBetweenCollectableSpawns = currentTripState.TimeBetweenCollectableSpawns;
        timeBetweenObstacleSpawns = currentTripState.TimeBetweenObstacleSpawns;
        RenderSettings.skybox = currentTripState.SkyboxMaterial;
        Material skyMat = currentTripState.SkyboxMaterial;
        skyMat.SetFloat("_Rotation", skyMat.GetFloat("_Rotation") + currentTripState.SkyboxRotationSpeed * Time.deltaTime);
        
    }

    private void SpawnCollectables(){
        Vector3 pos = new Vector3(Random.Range(-90.0f,-100.0f),2f,Random.Range(-6.0f,6.0f));
        Instantiate(currentTripState.Collectable,pos,Quaternion.identity);
    }

    private void SpawnObstacles(){
        int randomIndex = Random.Range(0,currentTripState.Obstacles.Count);
        Vector3 pos = new Vector3(Random.Range(-90.0f,-100.0f),0f,Random.Range(-6.0f,6.0f));
        Instantiate(currentTripState.Obstacles[randomIndex],pos,Quaternion.identity);
    }
}

[System.Serializable]
public class TripState{
    public string StateName;
    public float Curvature;
    public float HorizonWaveFrequency;
    public float MovementSpeed;
    public float TimeBetweenCollectableSpawns;
    public float TimeBetweenObstacleSpawns;
    public GameObject Collectable;
    public List<GameObject> Obstacles = new List<GameObject>();
    public Material SkyboxMaterial;
    public float SkyboxRotationSpeed;

    public TripState(string _StateName, float _Curvature, float _HorizonWaveFrequency, float _SkyboxRotationSpeed, float _MovementSpeed, GameObject _Collectable, List<GameObject> _Obstacles,Material _SkyboxMaterial, float _TimeBetweenCollectableSpawns = 3.0f,float _TimeBetweenObstacleSpawns = 1.5f){
        this.StateName = _StateName;
        this.Curvature = _Curvature;
        this.HorizonWaveFrequency = _HorizonWaveFrequency;
        this.MovementSpeed = _MovementSpeed;
        this.SkyboxRotationSpeed = _SkyboxRotationSpeed;
        this.TimeBetweenCollectableSpawns = _TimeBetweenCollectableSpawns;
        this.TimeBetweenObstacleSpawns = _TimeBetweenObstacleSpawns;
        this.Collectable = _Collectable;
        this.Obstacles = _Obstacles;
        this.SkyboxMaterial = _SkyboxMaterial;
    }
}

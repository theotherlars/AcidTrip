using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float trippiness = 0;
    [SerializeField] BendControllerRadial bendController;
    [SerializeField] List<GameObject> MushRooms = new List<GameObject>(); 
    [SerializeField] float timeBetweenItemSpawns = 1.5f;
    [SerializeField] float transitionTime = 2;
    [SerializeField] List<TripState> tripStates = new List<TripState>();
    TripState currentTripState;
    float timeSinceLastItemSpawn;
    GroundMovement groundMovement;

    private void Awake() {
        bendController = FindObjectOfType<BendControllerRadial>();
        groundMovement = FindObjectOfType<GroundMovement>();
        currentTripState = tripStates[0];

        bendController.HorizonWaves = true;
    }

    private void Update() {
        SetTrippiness(trippiness);
        UpdateTripValues();

        if(timeSinceLastItemSpawn > timeBetweenItemSpawns){
            timeSinceLastItemSpawn = 0;
            SpawnItems();
        }
        timeSinceLastItemSpawn += Time.deltaTime;
    }

    private void SetTrippiness(float newValue){
        if(newValue <= 0.2){
            currentTripState = tripStates[0];
            // bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 120, Time.deltaTime);
            // bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 20, Time.deltaTime);
        }
        else if(newValue > 0.2 && newValue <= 0.5){
            currentTripState = tripStates[1];
            // bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 120 * -1, Time.deltaTime);
            // bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 70, Time.deltaTime);
        }
        else if(newValue > 0.5 && newValue <= 0.7){
            currentTripState = tripStates[2];
            // bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 230 * -1, Time.deltaTime);
            // bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 100, Time.deltaTime);
        }
        else if(newValue > 0.5 && newValue <= 0.7){
            currentTripState = tripStates[3];
            // bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 230, Time.deltaTime);
            // bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * -1, Time.deltaTime);
        }
        else if(newValue > 0.7 && newValue <= 1){
            currentTripState = tripStates[4];
            // bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * -400, Time.deltaTime);
            // bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 100, Time.deltaTime);
        }
    }

    private void UpdateTripValues(){
        bendController.Curvature = Mathf.Lerp(bendController.Curvature, currentTripState.Curvature, Time.deltaTime * transitionTime);
        bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, currentTripState.HorizonWaveFrequency, Time.deltaTime * transitionTime);
        groundMovement.movementSpeed = currentTripState.MovementSpeed;
        
    }

    private void SpawnItems(){
        int randomIndex = Random.Range(0,MushRooms.Count);
        Vector3 pos = new Vector3(Random.Range(-20,-35),-0.2f,0);
        if(Random.value > 0.5){
            pos.z = -10;            
        }
        else{
            pos.z = 10;
        }
        Instantiate(MushRooms[randomIndex],pos,Quaternion.identity);
    }
}

[System.Serializable]
public class TripState{
    public string StateName;
    public float Curvature;
    public float HorizonWaveFrequency;
    public float MovementSpeed;

    public TripState(string _StateName, float _Curvature, float _HorizonWaveFrequency, float _MovementSpeed){
        this.StateName = _StateName;
        this.Curvature = _Curvature;
        this.HorizonWaveFrequency = _HorizonWaveFrequency;
        this.MovementSpeed = _MovementSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Range(0,1)] float trippiness = 0;
    [SerializeField] BendControllerRadial bendController;


    private void Awake() {
        bendController = FindObjectOfType<BendControllerRadial>();
        bendController.Curvature = 0;
        bendController.HorizonWaveFrequency = 0;
        bendController.HorizonWaves = true;
    }

    private void Update() {
        SetTrippiness(trippiness);
    }

    private void SetTrippiness(float newValue){
        if(newValue <= 0.2){
            bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 120, Time.deltaTime);
            bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 20, Time.deltaTime);
        }
        else if(newValue > 0.2 && newValue <= 0.5){
            bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 120 * -1, Time.deltaTime);
            bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 70, Time.deltaTime);
        }
        else if(newValue > 0.5 && newValue <= 0.7){
            bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 230 * -1, Time.deltaTime);
            bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 100, Time.deltaTime);
        }
        else if(newValue > 0.5 && newValue <= 0.7){
            bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * 230, Time.deltaTime);
            bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * -1, Time.deltaTime);
        }
        else if(newValue > 0.7 && newValue <= 1){
            bendController.Curvature = Mathf.Lerp(bendController.Curvature, newValue * -400, Time.deltaTime);
            bendController.HorizonWaveFrequency = Mathf.Lerp(bendController.HorizonWaveFrequency, newValue * 100, Time.deltaTime);
        }
    }
}

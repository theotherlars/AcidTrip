using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    public List<GameObject> healthObjects = new List<GameObject>();
    PlayerManager playerManager;
    private void Start() {
        playerManager = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        playerManager.onLoseHealth.AddListener(LoseHealth);
        playerManager.onGainHealth.AddListener(GainHealth);    
    }

    private void OnDisable() {
        playerManager.onLoseHealth.RemoveListener(LoseHealth);
        playerManager.onGainHealth.RemoveListener(GainHealth);
    }

    void LoseHealth(){
        for(int i = healthObjects.Count-1; i >= 0; i--){
            if(healthObjects[i].activeInHierarchy){
                healthObjects[i].SetActive(false);
                return;
            }
        }
    }

    void GainHealth(){
        for(int i = 0; i < healthObjects.Count; i++){
                if(!healthObjects[i].activeInHierarchy){
                    healthObjects[i].SetActive(true);
                    return;
                }
        }
    }
}

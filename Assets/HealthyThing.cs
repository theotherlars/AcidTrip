using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyThing : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.TryGetComponent(out PlayerManager playerManager)){
                playerManager.GainHealth();
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyThing : MonoBehaviour
{
    private void Update() {
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x , transform.rotation.y + Time.time * 100f, transform.eulerAngles.z);
    }
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player")){
            if(other.gameObject.TryGetComponent(out PlayerManager playerManager)){
                playerManager.GainHealth();
                Destroy(gameObject);
            }
        }
    }
}

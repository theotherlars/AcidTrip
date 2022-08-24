using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    float movementSpeed;
    GroundMovement groundMovement;

    bool triggered = false;

    private void Awake() {
        groundMovement = FindObjectOfType<GroundMovement>();
        Destroy(gameObject,10);
    }
    private void Update() {
        movementSpeed = groundMovement.movementSpeed;
        transform.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && !triggered){
            triggered = true;
            if(other.gameObject.TryGetComponent(out PlayerManager playerManager)){
                playerManager.LoseHealth();
            }
        }
    }
}

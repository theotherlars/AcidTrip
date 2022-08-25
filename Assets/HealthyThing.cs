using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthyThing : MonoBehaviour
{
    bool playerDead = false;
    [HideInInspector] public bool independentSpeed = false;
    float movementSpeed;

    GroundMovement groundMovement;
    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
        groundMovement = FindObjectOfType<GroundMovement>();
        Destroy(gameObject,10);
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    private void Update() {
        if(playerDead){return;}
        if(!independentSpeed){
            movementSpeed = groundMovement.movementSpeed;
        }

        transform.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
        
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

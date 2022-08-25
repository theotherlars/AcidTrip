using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [HideInInspector] public float movementSpeed;
    GroundMovement groundMovement;
    [HideInInspector] public bool independentSpeed = false;
    bool triggered = false;
    bool playerDead = false;

    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    private void Awake() {
        groundMovement = FindObjectOfType<GroundMovement>();
        Destroy(gameObject,10);
    }
    private void Update() {
        if(playerDead){return;}
        if(!independentSpeed) {
            movementSpeed = groundMovement.movementSpeed;
        }
        transform.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
    }
}

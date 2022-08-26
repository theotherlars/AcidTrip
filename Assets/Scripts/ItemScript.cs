using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    [HideInInspector] public float movementSpeed;
    GroundMovement groundMovement;
    [SerializeField] public bool independentSpeed = false;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] AudioClip deathClip;

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
        if(!independentSpeed){
            movementSpeed = groundMovement.movementSpeed;
        }

        transform.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Player") && !triggered){
            triggered = true;
            if(other.gameObject.TryGetComponent(out PlayerManager playerManager)){
                playerManager.LoseHealth();
            }
            Instantiate(deathParticles,transform.position,Quaternion.Euler(-45,90,0));
            // Play Audio here
            if(deathClip){
                AudioSource.PlayClipAtPoint(deathClip, transform.position);
            }
            Destroy(gameObject);
        }
    }
}

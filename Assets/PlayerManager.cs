using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance { get => instance;}
    
    public int playerMaxHealth = 3;
    public int playerHealth;
    bool alive = true;
    public UnityEvent onLoseHealth;
    public UnityEvent onGainHealth;
    public UnityEvent onDeath;
    public ParticleSystem deathParticle;
    public GameObject friedEgg;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        playerHealth = playerMaxHealth;
    }

    public void LoseHealth(){
        playerHealth -= 1;
        onLoseHealth.Invoke();
        if(playerHealth <= 0 && alive){
            Died();
        }
    }

    public void GainHealth(){
        playerHealth = Mathf.Clamp(playerHealth + 1, 0, playerMaxHealth);
        onGainHealth.Invoke();
    }

    private void Died(){
        alive = false;
        onDeath.Invoke();
        Instantiate(deathParticle,transform.position,Quaternion.identity);
        Vector3 pos = transform.position;
        pos.y += 1.5f;
        Instantiate(friedEgg, pos, Quaternion.identity);
        Destroy(gameObject);
    }

}

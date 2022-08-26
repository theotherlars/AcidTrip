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
    public AudioClip deathClip;

    public List<AudioClip> footstepClips = new List<AudioClip>();
    AudioSource audioSource;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
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

    private void Footsteps(){
        audioSource.PlayOneShot(footstepClips[Random.Range((int)0, footstepClips.Count)],0.1f);
    }

    private void Died(){
        alive = false;
        onDeath.Invoke();
        Instantiate(deathParticle,transform.position,Quaternion.identity);
        Vector3 pos = transform.position;
        Instantiate(friedEgg, pos, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathClip,transform.position,1.0f);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Collectibles : MonoBehaviour
{
    public static event Action OnCollected;
    ScoreManager scoreManager;
    
    bool playerDead = false;


    void Start(){
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
       scoreManager = FindObjectOfType<Canvas>().GetComponent<ScoreManager>();
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    void Update(){
        if(playerDead){return;}
        // transform.rotation = Quaternion.Euler(transform.rotation.x , transform.rotation.y + Time.time * 100f, transform.rotation.z);
        transform.eulerAngles = new Vector3 (transform.eulerAngles.x , transform.rotation.y + Time.time * 100f, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            OnCollected?.Invoke();
            scoreManager.AddPoint();
            Destroy(gameObject);
        }
    }
}

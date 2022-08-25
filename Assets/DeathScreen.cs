using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    [SerializeField] GameObject deathMenu;
    bool playerDead = false;

    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(TriggerDeathMenu);
    }

    private void OnDisable() {
        
        PlayerManager.Instance.onDeath.RemoveListener(TriggerDeathMenu);
    }

    void TriggerDeathMenu(){
        StartCoroutine("EnableDeathMenu");
        
    }

    IEnumerator EnableDeathMenu(){
        yield return new WaitForSeconds(4.0f);
        deathMenu.SetActive(true);
    }
}

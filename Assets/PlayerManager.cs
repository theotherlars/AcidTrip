using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playerMaxHealth = 3;
    public int playerHealth;
    bool alive = true;


    public void LoseHealth(){
        playerHealth -= 1;
        if(playerHealth <= 0 && alive){
            Died();
        }
    }

    public void GainHealth(){
        playerHealth = Mathf.Clamp(playerHealth + 1, 0, playerMaxHealth);
    }

    private void Died(){
        alive = false;
        print("Player Overdosed!");
    }

}

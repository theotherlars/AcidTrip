using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float movementSpeed;
    public float offsetBeforeMoved;
    public List<Transform> groundPieces = new List<Transform>();
    bool playerDead = false;

    private void Start() {
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    private void Update() {
        if(playerDead){return;}
        UpdateGround();
    }

    private void UpdateGround(){
        foreach(Transform ground in groundPieces){
            ground.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
            if(ground.position.x >= offsetBeforeMoved){
                ground.position -= new Vector3(20 * groundPieces.Count, 0, 0);
            }
        }
    }
}

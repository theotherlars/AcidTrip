using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float movementSpeed;
    public float offsetBeforeMoved;
    public List<Transform> groundPieces = new List<Transform>();

    void Update(){
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

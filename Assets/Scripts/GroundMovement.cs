using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float movementSpeed;
    public float offsetBeforeMoved;
    public Transform ground1;
    public Transform ground2;

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        ground1.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);
        ground2.position += new Vector3(2 * movementSpeed * Time.deltaTime, 0, 0);

        if(ground1.position.x >= offsetBeforeMoved){
            ground1.position -= new Vector3(40,0,0);
        }
        if(ground2.position.x >= offsetBeforeMoved){
            ground2.position -= new Vector3(40,0,0);
        }
        
        
    }
}

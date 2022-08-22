using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour {
    
    [Header("Interaction Stuff:")]
    [SerializeField]KeyCode interactionKey;
    [SerializeField]Transform interactionCheck;
    [SerializeField]float interactionCheckDistance;
    [SerializeField]LayerMask interactionLayer;

    [Header("Other Stuff:")]
    [SerializeField]public bool hasKeyCard;
    Door closeToDoor;
    private void Update() {
        if(closeToDoor && Input.GetKeyDown(interactionKey) && hasKeyCard){
            closeToDoor.ToggleDoor();
        }
        IsCloseToInteration();
    }

    private void IsCloseToInteration(){
        if(Physics.Raycast(interactionCheck.position,transform.forward,out RaycastHit hit,interactionCheckDistance,interactionLayer)){
            if(hit.transform.gameObject.TryGetComponent(out Door door)){
                closeToDoor = door;
            }
        }
    }
}
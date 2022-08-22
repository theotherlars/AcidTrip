using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
    [SerializeField] float openingSpeed = 1;
    [SerializeField] Vector3 openRotation, closedRotation;
    [SerializeField]float slideOpening;
    [SerializeField]Transform leftDoor;
    [SerializeField]Transform rightDoor;
    [SerializeField]LayerMask playerLayer;

    // [SerializeField] soAudio openAudio, closeAudio;
    bool isOpen;
    bool doOnce;
    // GameManager gm;
    Vector3 closedLeftDoor;
    Vector3 closedRightDoor;
    private void Start() {
        if(leftDoor && rightDoor){
            closedLeftDoor = leftDoor.localPosition;
            closedRightDoor = rightDoor.localPosition;
        }
    }
    
    IEnumerator Rotate(){
        // float timer = 0;
        // convert rotations to Quaternions
        Quaternion openRot = Quaternion.Euler(openRotation);
        Quaternion closedRot = Quaternion.Euler(closedRotation);
        Quaternion endRot = isOpen ? openRot : closedRot;


        //TODO: //Audio stuff
        // soAudio audioToPlay = isOpen ? openAudio : closeAudio;
        // AudioManager.Instance.PlayAudio(audioToPlay);

        while(transform.localRotation.y != endRot.y){
        
            // If door is open, set to closed rotation. If door is closed, set to open rotation.
            transform.localRotation = isOpen ? Quaternion.Slerp(transform.localRotation, openRot, openingSpeed * Time.deltaTime) : Quaternion.Slerp(transform.localRotation, closedRot, openingSpeed * Time.deltaTime);
            // timer += Time.deltaTime;
            yield return null;
        
        }
        
        // Make sure that the transform is set to correct rotation
        //  transform.localRotation = isOpen ? openRot : closedRot;
    }
   
    IEnumerator DualSlide(){
        Vector3 openPosLeft = leftDoor.localPosition;
        openPosLeft.x -= slideOpening;

        Vector3 openPosRight = rightDoor.localPosition;
        openPosRight.x += slideOpening;

        //TODO: //Audio stuff
        // soAudio audioToPlay = isOpen ? openAudio : closeAudio;
        // AudioManager.Instance.PlayAudio(audioToPlay);
        if(isOpen){
            while(leftDoor.localPosition.x != openPosLeft.x && rightDoor.localPosition.x != openPosRight.x){
                leftDoor.localPosition = Vector3.Slerp(leftDoor.localPosition,openPosLeft, openingSpeed * Time.deltaTime);
                rightDoor.localPosition = Vector3.Slerp(rightDoor.localPosition,openPosRight, openingSpeed * Time.deltaTime);
                yield return null;
            }
        }
        
        if(!isOpen){
            while(leftDoor.localPosition.x != closedLeftDoor.x && rightDoor.localPosition.x != closedRightDoor.x){
                leftDoor.localPosition = Vector3.Slerp(leftDoor.localPosition,closedLeftDoor, openingSpeed * Time.deltaTime);
                rightDoor.localPosition = Vector3.Slerp(rightDoor.localPosition,closedRightDoor, openingSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
   public void ToggleDoor(){
        isOpen = !isOpen;
        StartCoroutine(DualSlide());
    }
}
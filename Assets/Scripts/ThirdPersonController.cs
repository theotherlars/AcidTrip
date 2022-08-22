using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Inputs:")]
    [SerializeField]KeyCode runKey;
    [SerializeField]KeyCode jumpKey;
    
    [Header("Speeds:")]
    [SerializeField]float movementSpeed;
    [SerializeField]float runSpeed;

    [Header("Rotation:")]
    [SerializeField]float rotationSmoothTime;
    
    [Header("Jump:")]
    [SerializeField]int jumpHeight;
    [SerializeField]bool enableAdjustableJump;
    [SerializeField]float adjustableJumpTime;
    [SerializeField]float adjustableJumpMultiplier;
    
    [Header("Double Jump Stuff:")]
    [SerializeField]bool allowDoubleJump;
    [SerializeField]int allowedExtraJumps;
    [SerializeField]bool onlyAllowOnDecline; 

    [Header("Gravity:")]
    [SerializeField]float gravityScale;
    public Vector3 velocity;
    
    [Header("GroundCheck:")]
    [SerializeField]float groundCheckRadius;
    [SerializeField]LayerMask whatIsGround;
    public bool isGrounded;
    
    [Header("Checkpoints:")]
    [SerializeField]Transform groundCheckPos;
    // [SerializeField]Vector3 groundCheckPos;

    // PRIVATE
    Vector3 direction;
    CharacterController cc;
    Camera cam;
    float currentTurnVelocity;
    int currentJumpCount;
    float adjustableJumpTimeCounter;
    

    // CONSTANTS
    const float gravityConst = -9.81f;

    void Start(){
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
        
        groundCheckPos.localPosition = new Vector3(0,-cc.height/2,0);

        // SYSTEM STUFF
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
    }

    void Update(){
        isGrounded = IsGrounded();
        HandleInput();
        HandleGravity();
        HandleMovemet();        
        HandleJump();
    }

    void HandleInput(){
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        direction = new Vector3(xInput,0,zInput);
    }

    void HandleGravity(){
        if(isGrounded && velocity.y < 0.0f){
            velocity = Vector3.down * 2;            
        }
        else{
            velocity.y += gravityConst * gravityScale * Time.deltaTime;
        }
        cc.Move(velocity * Time.deltaTime);
    }
    
    private void HandleMovemet(){
        if(direction.magnitude > 0){
            if(Input.GetKey(runKey)){
                Move(runSpeed);
                return;
            }
            Move(movementSpeed);     
        }
    }

    private void HandleJump(){
        // cc.Move(velocity * Time.deltaTime); // Perform jump movement

        if(isGrounded){
            adjustableJumpTimeCounter = adjustableJumpTime;
        }
        else{
            adjustableJumpTimeCounter -= Time.deltaTime;
        }

        if(adjustableJumpTimeCounter > 0f  && Input.GetKeyDown(jumpKey)){ //is adjustableJumpTimeCounter greater than 0, it means the player is grounded
            Jump();
        }

        if(enableAdjustableJump){
            if(Input.GetKeyUp(jumpKey) && velocity.y > 0.0f){ // releasing jumpkey while on the way up, greater gravity down
                velocity.y *= adjustableJumpMultiplier;
                adjustableJumpTimeCounter = 0f;
            }
        }

        // DOUBLE JUMP             
        if(allowDoubleJump){
            if(Input.GetKeyDown(jumpKey) && !isGrounded && currentJumpCount < allowedExtraJumps){
                if(onlyAllowOnDecline && velocity.y < 0.0f){
                    Jump();
                }
                if(!onlyAllowOnDecline){
                    Jump();
                }
            }
        }
    }
    private void Move(float speed){        
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref currentTurnVelocity,rotationSmoothTime);
        transform.rotation = Quaternion.Euler(0,angle,0);
        Vector3 movementDirection = Quaternion.Euler(0,targetAngle,0) * Vector3.forward;
        Vector3 movement = movementDirection * speed * Time.deltaTime;
        cc.Move(movement);
    }

    void Jump(){
        currentJumpCount += 1;
        // jetPackTimer = 0;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityConst * gravityScale);
    }
    bool IsGrounded(){
        bool val = false;
        val = Physics.CheckSphere(groundCheckPos.position,groundCheckRadius,whatIsGround);
        if(val){currentJumpCount = 0;}
        return val;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(groundCheckPos.position,groundCheckRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{
    public float jumpForce = 4;
    

    private Rigidbody rb;
    Rigidbody m_Rigidbody;
    public float Speed = 5f;
    [SerializeField]float lockToXPosition;
    public BoxCollider playerCollider;
    public LayerMask whatIsGround;
    private Vector3 movementOffSet;
    bool playerDead = false;
    public Transform wheelBarrow;
    public float maxWheelbarrowRot;
    public float rotationSpeed;
    Transform target;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Rigidbody.freezeRotation = true;
        rb = GetComponent<Rigidbody>();
        PlayerManager.Instance.onDeath.AddListener(delegate {playerDead=true;});
    }

    private void OnDisable() {
        PlayerManager.Instance.onDeath.RemoveListener(delegate {playerDead=true;});
    }

    

    void Update()
    {
        if(playerDead){return;}
        float h = Input.GetAxisRaw("Horizontal");
        if(h > 0){
            transform.eulerAngles = new Vector3(0,-90,Mathf.Lerp(transform.eulerAngles.z,10,rotationSpeed) * -1);        
        }
        else if(h < 0){
            transform.eulerAngles = new Vector3(0,-90,Mathf.Lerp(transform.eulerAngles.z, 10,rotationSpeed));        
        }
        else{
            transform.eulerAngles = new Vector3(0,-90,0);        
        }
        
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, h * Speed);
        

        Jump();
    }
    private void Jump()
    {
        bool jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);
        if (jumpKeyPressed && isGrounded())
        {
            Vector3 jumpVector = Vector3.up * jumpForce;
            jumpVector.x = rb.velocity.x;
            jumpVector.z = rb.velocity.z;
            rb.velocity = jumpVector;
             
        }
    }

    private bool isGrounded()
    {
        //bool isGrounded = Physics.Raycast(transform.position, -gameObject.transform.up, playerCollider.bounds.extents.y + 1f);
        bool isGrounded = Physics.Raycast(transform.position, -gameObject.transform.up, 1, whatIsGround);
        return isGrounded;
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{
    public float jumpForce = 4;
    

    private Rigidbody rb;
    Rigidbody m_Rigidbody;
    public float Speed = 5f;

    public BoxCollider playerCollider;
    public LayerMask whatIsGround;

    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_Rigidbody.freezeRotation = true;
        rb = GetComponent<Rigidbody>();
    }

    

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, h * Speed);
        // rb.position = new Vector3(0,0, rb.position.z + h * Speed * Time.deltaTime);

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


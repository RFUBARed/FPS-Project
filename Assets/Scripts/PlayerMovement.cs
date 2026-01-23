using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;//inptu system

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;//movement speed
    public float jumpForce = 5f;//jump impulse, verticle force applied to player

    public Transform groundCheck;//EmptyObjcet placed at players feet(child of player)
    public float groundDistance = 0.4f;//sphere radius for ground test
    public LayerMask groundMask;//set to "Ground" layer in Inspector

    private Rigidbody rb;//player rigid body
    private Vector2 moveInput;//WASD/Arrow as (x,y)
    private bool isGrounded;//true while on ground

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb=GetComponent<Rigidbody>();//cache rigidbody
    }

    void Update()
    {
        CheckGround();//update ground state each frame/tick
    }

    void FixedUpdate()
    {
        MovePlayer();//physics friendly movement tick
    }

    void OnJump()   //action "jump"-->OnJump (case sensitive
    {
        if (isGrounded)//only jump when grounded
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);//upward impluse
    }

    void OnMovement(InputValue value)//action "Movement"--> OnMOvement (case sensitive)
    { 
        moveInput=value.Get<Vector2>();//read Vector2: x=A/D, y=W/S (and corresponding arrow keys)
    }

    void MovePlayer()
    { 
        //convert 2d input to world space using player right/forward
        Vector3 direction = (transform.right * moveInput.x) + (transform.forward*moveInput.y);
        direction = direction.normalized;//avoid faster diagonal movement

        //use linearVelocity (renamed from velocity); preserve Y(gravoity/jump)
        rb.linearVelocity = new Vector3(direction.x * moveSpeed, rb.linearVelocity.y, direction.z * moveSpeed);   
    }

    void CheckGround()
    {
        if (groundCheck == null)//safety:require a groundCheck transform
        {
            isGrounded = false;//not grounded if probe missing
            return;
        }

        //true if sphere overlaps any collider on groundMask within groundDistance
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    }

}

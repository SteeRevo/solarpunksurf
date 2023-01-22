using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed = 12f;
    
    [SerializeField]
    private float acceleration = 5;

    [SerializeField]
    private float turnTorque = 150;

    private InputManager playerActions;

    [SerializeField]
    private Camera playerCamera;

    Vector2 currentMovement;
    Vector3 moveVector;

    [SerializeField]
    private Rigidbody rb;


    private float moveSpeed;
    
    [SerializeField]
    private Transform moveSphere;

    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [SerializeField]
    private float jumpForce;
    

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;
    bool isGrounded;

    bool jumpInput = false;
    



    private void Awake()
    {
        playerActions = new InputManager();

        playerActions.Player.Move.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
        playerActions.Player.Jump.performed += ctx => jumpInput = true;
    }

    private void Start() {
            rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        currentMovement = playerActions.Player.Move.ReadValue<Vector2>();
        moveVector = new Vector3(currentMovement.x, 0, currentMovement.y).normalized;
        transform.position = rb.transform.position;

        

        float boardRotation = moveVector.x * turnTorque * Time.deltaTime;
        transform.Rotate(0, boardRotation, 0, Space.World);

        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);

        if(isGrounded && moveVector.y < 0){
            moveVector.y = -2f;
        }

        if(isGrounded && jumpInput)
        {
            Debug.Log("Jump");
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            jumpInput = false;
        }
    
        if(moveVector.z > 0){
            moveSpeed += acceleration;
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        }

        else{
            moveSpeed = 5;
        }

        if(moveVector != Vector3.zero){
            rb.AddForce(transform.forward * moveVector.z * moveSpeed, ForceMode.Acceleration);
        }
        
        //Debug.Log(moveVector);

    }

    void Jump()
    {
        Debug.Log("Jump");
    }


    private void OnEnable()
    {
        playerActions.Player.Enable();
    }
    

    private void OnDisable() {
        playerActions.Player.Disable();    
    }

    
}

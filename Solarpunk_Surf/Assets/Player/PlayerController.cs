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

    [SerializeField]
    private float fallMultiplier = 5f;

    [SerializeField]
    private float lowjumpMultiplier = 2.0f;
    

    
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
    bool isBoosting = false;
    float defaultTurnTorque;
    float originalSpeed;
    



    private void Awake()
    {
        playerActions = new InputManager();

        playerActions.Player.Move.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
        playerActions.Player.Jump.performed += _ => jumpInput = true;
        playerActions.Player.Jump.canceled += _ => jumpInput = false;
        playerActions.Player.Boost.performed += _ => isBoosting = true;
        playerActions.Player.Boost.canceled += _ => isBoosting = false;
    
        originalSpeed = maxSpeed;
        defaultTurnTorque = turnTorque;
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

        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);


        if(isGrounded && jumpInput)
        {
            Debug.Log("Jump");
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowjumpMultiplier - 1) * Time.deltaTime; 
        }
        else if (rb.velocity.y > 0 && !isGrounded && !jumpInput)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
        }
    
        if(moveVector.z > 0){
            moveSpeed += acceleration;
            moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        }

        else{
            moveSpeed = 5;
        }

        if(isBoosting)
        {
            maxSpeed = 30f;
            moveSpeed = maxSpeed;
            Debug.Log("Is boosting");
        }
        else{
            maxSpeed = originalSpeed;
        }

        if(moveVector != Vector3.zero){
            rb.AddForce(transform.forward * moveVector.z * moveSpeed, ForceMode.Acceleration);
        }
        if(!isGrounded)
        {
            turnTorque = 200;
        }
        else{
            turnTorque = defaultTurnTorque;
        }
        float boardRotation = moveVector.x * turnTorque * Time.deltaTime;
        transform.Rotate(0, boardRotation, 0, Space.World);
        
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
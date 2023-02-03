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

    [SerializeField]
    private float boostForce;

    [SerializeField]
    private ParticleSystem particles;
    

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

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
        moveVector = matrix.MultiplyPoint3x4(moveVector);

        if(playerActions.Player.QuickDash.triggered)
        {
            Debug.Log("quick dash");
            rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
        }

        if(moveVector != Vector3.zero)
        {
           

            var relative = (transform.position + moveVector) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnTorque * Time.deltaTime);
            
            if(!particles.isPlaying) particles.Play();
            Debug.Log("emitting particles");
        }
        else{
            if(particles.isPlaying) particles.Stop();
            Debug.Log("partciles stopped");
        }

        particles.transform.position = new Vector3(transform.position.x-2, transform.position.y - 1, transform.position.z+2);
        particles.transform.rotation = new Quaternion(180, transform.rotation.y, 0, 1);
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);


        Jump(isGrounded);

        Boost();

        Dash();

        


        moveSpeed += acceleration;
        moveSpeed = Mathf.Min(moveSpeed, maxSpeed);
        
        if(moveVector != Vector3.zero){
            rb.AddForce(moveVector * moveSpeed, ForceMode.Acceleration);
        }
       
        if(!isGrounded)
        {
            turnTorque = 200;
        }
        else{
            turnTorque = defaultTurnTorque;
        }
        //float boardRotation = moveVector.x * turnTorque * Time.deltaTime;
        //transform.Rotate(0, boardRotation, 0, Space.World);
        
        //Debug.Log(moveVector);


    }

    void Jump(bool isGrounded)
    {
        if(isGrounded && jumpInput)
        {
            Debug.Log("Jump");
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
        if(rb.velocity.y < 0 && !isGrounded)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowjumpMultiplier - 1) * Time.deltaTime; 
        }
        else if (rb.velocity.y > 0 && !isGrounded && !jumpInput)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; 
        }
    
        
    }

    private void Boost()
    {
        if(isBoosting)
        {
            maxSpeed = 30f;
            moveSpeed = maxSpeed;
            Debug.Log("Is boosting");
        }
        else{
            maxSpeed = originalSpeed;
        }
    }

    private void Dash()
    {
       return;

    }



    private void OnEnable()
    {
        playerActions.Player.Enable();
    }
    

    private void OnDisable() {
        playerActions.Player.Disable();    
    }

    
}

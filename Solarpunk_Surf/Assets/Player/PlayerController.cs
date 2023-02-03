using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 using UnityEngine.UI;


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
    
    // slider //
    [SerializeField]
    private Slider BoostMeter;

    [HideInInspector] public BoostMeterScript _boostMeterScript;

    //** **//

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
        _boostMeterScript = GameObject.Find("Slider").GetComponent<BoostMeterScript>();
       
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        currentMovement = playerActions.Player.Move.ReadValue<Vector2>();
        moveVector = new Vector3(currentMovement.x, 0, currentMovement.y).normalized;
        transform.position = rb.transform.position;

        if(playerActions.Player.QuickDash.triggered)
        {
            Debug.Log("quick dash");
            rb.AddForce(transform.forward * boostForce, ForceMode.Impulse);
        }

        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);


        Jump(isGrounded);

        Boost();

        Dash();

        


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
        
        // StartCoroutine(_boostMeterScript.regenBoostMeter());
        if(isBoosting && BoostMeter.value > 1)
        {
            maxSpeed = 30f;
            moveSpeed = maxSpeed;
            Debug.Log("Is boosting");
            BoostMeter.value -= 1;
            Debug.Log("inside player boost,"+ BoostMeter.value);
        }
        else{
            Debug.Log("stopped boosting boosting" + maxSpeed);
            maxSpeed = originalSpeed;
        }
        _boostMeterScript.Update();
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

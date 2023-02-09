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
    private float dashForce;

    [SerializeField]
    private ParticleSystem particles;
    

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;
    bool isGrounded;

    bool jumpInput = false;
    bool isBoosting = false;
    bool overheated = false;

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
        _boostMeterScript = BoostMeter.GetComponent<BoostMeterScript>();
       
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

        if(playerActions.Player.QuickDash.triggered && !overheated)
        {
            if(moveVector == Vector3.zero)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);
            }
            else
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(moveVector * dashForce, ForceMode.VelocityChange);
            }
           
            BoostMeter.value -= 50;
            if(BoostMeter.value <= 0)
            {
                overheated = true;
            }
            
        }
        
        
        currentMovement = playerActions.Player.Move.ReadValue<Vector2>();
        moveVector = new Vector3(currentMovement.x, 0, currentMovement.y).normalized;
        transform.position = rb.transform.position;

        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, -45, 0));
        moveVector = matrix.MultiplyPoint3x4(moveVector);


        if(moveVector != Vector3.zero)
        {
           

            var relative = (transform.position + moveVector) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnTorque * Time.deltaTime);
            
            //if(!particles.isPlaying) particles.Play();
            //Debug.Log("emitting particles");
        }
        else{
            //if(particles.isPlaying) particles.Stop();
            //Debug.Log("partciles stopped");
        }

        //particles.transform.position = new Vector3(transform.position.x-2, transform.position.y - 1, transform.position.z+2);
        //particles.transform.rotation = new Quaternion(180, transform.rotation.y, 0, 1);

        if(overheated && BoostMeter.value == 100)
        {
            overheated = false;
        }
        
    }

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);


        Jump(isGrounded);

        Boost();

        
       

      



         


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
        
        // StartCoroutine(_boostMeterScript.regenBoostMeter());
        if(isBoosting && BoostMeter.value > 1 && !overheated) 
        {
            maxSpeed = 30f;
            moveSpeed = maxSpeed;
            Debug.Log("Is boosting");
            BoostMeter.value -= 1;
            Debug.Log("inside player boost,"+ BoostMeter.value);
            if(BoostMeter.value <= 1)
            {
                overheated = true;
            }
        }
        
        else{
            //Debug.Log("stopped boosting boosting" + maxSpeed);
            maxSpeed = originalSpeed;
        }
        _boostMeterScript.Update();
    }




    private void OnEnable()
    {
        playerActions.Player.Enable();
    }
    

    private void OnDisable() {
        playerActions.Player.Disable();    
    }

    
}

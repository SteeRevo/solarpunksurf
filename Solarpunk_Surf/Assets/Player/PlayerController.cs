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

     [SerializeField]
    private Image BoostSun;

    

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
    private ParticleSystem ripple;

    [SerializeField]
    private Shader waterShader;
    

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;
    bool isGrounded;
    bool playParticles = false;

    bool jumpInput = false;
    bool isBoosting = false;
    bool overheated = false;

    float defaultTurnTorque;
    float originalSpeed;
    Color32 color = new Color(0.5f, 1f, 1f, 1f);

    public float boostCounter = 0.0f;
    public float noBoostCounter = 0.0f;

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
        ripple.Stop();
    }

    private void Start() {
        _boostMeterScript = BoostMeter.GetComponent<BoostMeterScript>();
       
        rb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        BoostSun.fillAmount = BoostMeter.value/100;


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
           
            BoostMeter.value -= 25;
            
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

        //Vector3 ripplePlacement = new Vector3(transform.position.x+0.5f, transform.position.y, transform.position.z - 1);
        //ripple.transform.position = ripplePlacement;


        if(moveVector != Vector3.zero)
        {
           

            var relative = (transform.position + moveVector) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnTorque * Time.deltaTime);
            
            if(!playParticles && isGrounded){
                ripple.Play();
                playParticles = true;
            }
            else if(!isGrounded)
            {
                ripple.Stop();
                playParticles = false;
            }
            
           

            
    
        }
        else{
            if(playParticles){
                ripple.Stop();
                playParticles = false;
            }
            
        }
        
        
       

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
        
        if(isBoosting && BoostMeter.value > 1 && !overheated) 
        {
            noBoostCounter = 0.0f;
            boostCounter += 1f * Time.deltaTime;
            Debug.Log("boostCounter: "+ boostCounter);
            maxSpeed = 40f;
            moveSpeed = maxSpeed;
            // Debug.Log("Is boosting");
            BoostMeter.value -= 1;
            // Debug.Log("inside player boost,"+ BoostMeter.value);
            if(BoostMeter.value <= 1)
            {
                overheated = true;
            }
        }
        else if (!isBoosting && BoostMeter.value > 1 && !overheated) {
            // Debug.Log("!!stopped boosting counter: "+ boostCounter);
            boostCounter = 0.0f;
            noBoostCounter += 2f * Time.deltaTime;
            // Debug.Log("!!noBoostCounter: "+ noBoostCounter);
            if (noBoostCounter > 0.0 && noBoostCounter < 2.0) {
                _boostMeterScript.Invoke("regenBoostMeter", 2.0f);
            } 
            else if (noBoostCounter > 2.0) {
                _boostMeterScript.regenBoostMeter();
                //maxSpeed = originalSpeed;
            }
            maxSpeed = originalSpeed;
        } else {
            _boostMeterScript.regenBoostMeter();
            maxSpeed = originalSpeed;
        }

    }
    public IEnumerator waitTimer() {
        yield return new WaitForSeconds(0.5f);
    }

    // public void regenBoostMeter() {
    //     _boostMeterScript.boostMeterSlider.value += 10 * Time.deltaTime;
    //     _boostMeterScript.boostMeterSlider.value = Mathf.Clamp(_boostMeterScript.boostMeterSlider.value, 0, 100);
    // }

    /*
    private void createRipple(int start, int end, int delta, float speed, float size, float lifetime)
    {
        Vector3 forward = ripple.transform.eulerAngles;
        forward.y = start;
        ripple.transform.eulerAngles = forward;
        Color32 color = new Color(0.5f, 1f, 1f, 1f);
        
        for(int i = 0; i < end; i+=delta)
        {
            ripple.Emit(transform.position + ripple.transform.forward * 0.5f, ripple.transform.forward * speed, size, lifetime, color);
            ripple.transform.eulerAngles += Vector3.up * 3;
        }
    }*/


    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.Player.Enable();
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.Player.Disable();    
    }

    
}

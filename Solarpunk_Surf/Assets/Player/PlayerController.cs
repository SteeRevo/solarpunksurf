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

    // [SerializeField]
    //private Image BoostSun;


    [HideInInspector] public BoostMeterScript _boostMeterScript;

    //** **//

    public InputManager playerActions;


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

    
    private PlayerAudioManager audioManager;

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

    public bool inPause = false;
    private bool inConvo = false;

    public delegate void InteractAction();
    public static event InteractAction OnInteract;
    private bool isCollected = false;
    private bool canJumpPause = false;

    private void Awake()
    {
        playerActions = new InputManager();

        playerActions.Player.Move.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
        playerActions.Player.Jump.performed += _ => jumpInput = true;
        playerActions.Player.Jump.canceled += _ => jumpInput = false;
        playerActions.Player.Boost.performed += _ => isBoosting = true;
        playerActions.Player.Boost.canceled += _ => isBoosting = false;
        playerActions.Player.Interact.performed += _ => OnInteract();
    
        originalSpeed = maxSpeed;
        defaultTurnTorque = turnTorque;
        ripple.Stop();
    }

    private void Start() {
        _boostMeterScript = BoostMeter.GetComponent<BoostMeterScript>();
        rb.transform.parent = null;

        audioManager = GetComponent<PlayerAudioManager>();

        overheated = false;
        // tried to do this with a setter it crashed game so idk
        _boostMeterScript.showOverheated(overheated);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inPause && !inConvo){
            if(playerActions.Player.QuickDash.triggered && !overheated)
            {
                audioManager.playDashSound();
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
            }

            if(BoostMeter.value <= 0)
            {
                overheated = true;
                _boostMeterScript.showOverheated(overheated);
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

                    audioManager.playMoveLoop();
                    //audioSource.Play();
                }
                else if(!isGrounded)
                {
                    ripple.Stop();
                    playParticles = false;

                    audioManager.pauseMoveLoop();
                    //audioSource.Pause();
                }
                
            

                
        
            }

            else{
                if(playParticles){
                    ripple.Stop();
                    audioManager.pauseMoveLoop();
                    playParticles = false;
                }
                
            }
            
            
        

            if(overheated && BoostMeter.value == 100)
            {
                overheated = false;
                 _boostMeterScript.showOverheated(overheated);
            }

            

            
       
        }
        else{
            audioManager.pauseMoveLoop();
            return;
        }
        
        
    
        
        
    }

    

    private void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(moveSphere.position, groundDistance, groundMask);
        if(!inPause && !inConvo)
        {
            Jump(isGrounded);
                            
            Boost();
            
        }
        


       



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

            audioManager.playJumpSound();
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
            maxSpeed = 80f;
            moveSpeed = maxSpeed;
            // Debug.Log("Is boosting");
            BoostMeter.value -= 0.5f;
            // Debug.Log("inside player boost,"+ BoostMeter.value);
            if(BoostMeter.value <= 1)
            {
                overheated = true;
                _boostMeterScript.showOverheated(overheated);
            }

            audioManager.boostMoveLoop();
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
            // audioManager.restoreMoveLoop();
        } else {
            _boostMeterScript.regenBoostMeter();
            maxSpeed = originalSpeed;
            audioManager.restoreMoveLoop();
        }

    }

    public IEnumerator waitTimer() {
        yield return new WaitForEndOfFrame();
        
    }

    // public void regenBoostMeter() {
    //     _boostMeterScript.boostMeterSlider.value += 10 * Time.deltaTime;
    //     _boostMeterScript.boostMeterSlider.value = Mathf.Clamp(_boostMeterScript.boostMeterSlider.value, 0, 100);
    // }


    // public void OnMovementEnable()
    // {
    //     Debug.Log("player movement enabled");
    //     playerActions.Player.Enable();
        
    // }
    //  public void OnUIEnable()
    // {
    //     Debug.Log("player UI enabled");
    //     playerActions.UI.Enable();
        
    // }

    
    // // changed this from private to public so the dialogue trigger can access
    // public void OnMovementDisable() {
    //     Debug.Log("diabled player movement");
    //     playerActions.Player.Disable();
         
    // }

    // public void OnUIDisable() {
    //     Debug.Log("diabled player UI");
    //     playerActions.UI.Disable();
         
    // }
    public void OnEnable()
    {
        Debug.Log("player movement enabled");
        playerActions.Player.Enable();
        Collect.OnCollect += collectItem;
        Menu_Controls.OnPause += changePause;
        Dialogue.inDialogue += changeDialogue;
        
    }
    
    // changed this from private to public so the dialogue trigger can access
    public void OnDisable() {
        Debug.Log("diabled player movement");
        playerActions.Player.Disable();
        // playerActions.UI.Disable(); 
        inPause = true;  
        Debug.Log("In pause is true");
        Collect.OnCollect -= collectItem;
        Menu_Controls.OnPause -= changePause;
        Dialogue.inDialogue -= changeDialogue;
        
    }

    

    public void changePause()
    {
        inPause = !inPause;
        Debug.Log("Here is pause: " + inPause);
        
    }

    public void changeDialogue()
    {
        inConvo = !inConvo;
        Debug.Log("Here is pause: " + inPause);
        
    }

    void collectItem()
    {
        isCollected = true;
    }

    public bool checkItem()
    {
        return isCollected;
    }


    
}

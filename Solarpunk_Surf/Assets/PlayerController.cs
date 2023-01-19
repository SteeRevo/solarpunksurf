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
    private float brakeSpeed = 2;

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
    

    public float turnSmoothTime = 1f;
    float turnSmoothVelocity;

    



    private void Awake()
    {
        playerActions = new InputManager();

        playerActions.Player.Move.performed += ctx => currentMovement = ctx.ReadValue<Vector2>();
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


    private void OnEnable()
    {
        playerActions.Player.Enable();
    }
    

    private void OnDisable() {
        playerActions.Player.Disable();    
    }

    public void movePlayer()
    {
       
    }
}

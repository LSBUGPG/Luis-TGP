using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;


    private CustomInput input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    public float moveSpeed = 10f;
    public float turretRotationSpeed = 10f;

    //This line gets the transform value of the turret
    public Transform turretParent;

    public UnityEvent<Vector2> OnMoveTurret = new UnityEvent<Vector2>();

    
    public List<Transform> turretBarrel;
    public GameObject bulPrefab;
    public GameObject starBulPrefab;
    public GameObject squareBulPrefab;
    public float reloadDelay = 1f;
    public float bulCount = 100f;
    public float starBulCount = 5f;
    public float squareBulCount = 1f;
    public bool starPower = false;
    public bool squarePower = false;

    private bool canShoot = true;
    public Collider2D tankColliders;
    private float currentDelay = 0f;

    private ObjectPool BulletPool;
    [SerializeField]
    private int bulletpoolCount = 10;

    private void Awake()
    {

        tankColliders = GetComponentInParent<Collider2D>();

        input = new CustomInput();
        rb= GetComponent<Rigidbody2D>();  
        if (mainCamera == null )
        {
            mainCamera = Camera.main;
        }
        

        BulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        BulletPool.Initialize(bulPrefab, bulletpoolCount);
    }

    private void OnEnable()
    {
        
        input.Enable();
        input.Player.Movement.performed += OnMovementPerformed;
        input.Player.Movement.canceled += OnMovementCancelled;
        input.Player.Shoot.performed += OnShootPerformed;
        input.Player.Shoot.canceled += OnShootCancelled;
        input.Player.Rotate.performed += OnRotatePerformed;
        input.Player.Rotate.canceled += OnRotateCancelled;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= OnMovementPerformed;
        input.Player.Shoot.performed -= OnShootPerformed;
        input.Player.Movement.canceled -= OnMovementCancelled;
        input.Player.Shoot.canceled -= OnShootCancelled;
        input.Player.Rotate.performed -= OnRotatePerformed;
        input.Player.Rotate.canceled -= OnRotateCancelled;
    }
    private void Update()
    {
        GetTurretMovement();
        if (starBulCount <= 0)
        {
            starPower = false;
        }
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            if (currentDelay < 0f && bulCount > 0) 
            {
                canShoot = true;
            }
        }
    }
    public void StarPowerupActive()
    {
        starPower = true;
        starBulCount = 5;

    } 
    public void SquarePowerupActive()
    {
        squarePower = true;   

    }

    private void FixedUpdate()
    {
        
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value) 
    {
        moveVector = Vector2.zero;
    }

    public AudioSource bulSound;
    private void OnShootPerformed(InputAction.CallbackContext value)
    {
        Debug.Log("I'm shooting");
        if (canShoot & starPower == false & squarePower == false)
        {
            bulCount -= 1;
            canShoot = false;
            currentDelay = reloadDelay;
           bulSound.Play();

            foreach (var barrel in turretBarrel)
            {
                GameObject bullet = BulletPool.CreateObject();
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), tankColliders);             
            }

        }

        if (canShoot & starPower == true)
        {
            starBulCount -= 1;
            canShoot = false;
            currentDelay = reloadDelay;
            bulSound.Play();

            foreach (var barrel in turretBarrel)
            {
                GameObject StarBul = Instantiate(starBulPrefab);
                StarBul.transform.position = barrel.position;
                StarBul.transform.localRotation = barrel.rotation;
                StarBul.GetComponent<StarBul>().Initialize();
                Physics2D.IgnoreCollision(StarBul.GetComponent<Collider2D>(), tankColliders);
            }

        }

        if (canShoot & squarePower == true)
        {
            squareBulCount -= 1;
            canShoot = false;
            currentDelay = reloadDelay;


            foreach (var barrel in turretBarrel)
            {
                GameObject SquareBul = Instantiate(squareBulPrefab);
                SquareBul.transform.position = barrel.position;
                SquareBul.transform.localRotation = barrel.rotation;
                SquareBul.GetComponent<SquareBul>().Initialize();
                Physics2D.IgnoreCollision(SquareBul.GetComponent<Collider2D>(), tankColliders);
            }

        }

    }
    private void OnShootCancelled(InputAction.CallbackContext value)
    {
        Debug.Log("I'm not shooting");

    }

    void OnRotatePerformed(InputAction.CallbackContext value)
    {
        /*Debug.Log($"mouse move performed {value.ReadValue<Vector2>()}");
        Vector3 mousePosition = value.ReadValue<Vector2>();
        OnMoveTurret?.Invoke(GetMousePosition(mousePosition));*/

        var turretDirection = (Vector3)pointerPosition - turretParent.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);

    }
    void OnRotateCancelled(InputAction.CallbackContext value)
    {
        Debug.Log($"mouse move cancelled {value.ReadValue<Vector2>()}");
    }


    private void GetTurretMovement()
    {

    }

    private Vector2 GetMousePosition(Vector3 mousePosition)
    {
        mousePosition.z = mainCamera.nearClipPlane;
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        return mouseWorldPosition;

        
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        /*var turretDirection = (Vector3)pointerPosition - turretParent.position;
        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;
        var rotationStep = turretRotationSpeed * Time.deltaTime;
        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0,0,desiredAngle),rotationStep);*/

    }

}

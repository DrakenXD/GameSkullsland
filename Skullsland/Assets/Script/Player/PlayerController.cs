using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject light;

    [Header("Input")]

    public LayerMask layerHit;
    private Vector3 _rotMouse;
    private Vector2 _rotStick;
    private Vector2 move;
    private float rotationspeed;
    private float lastAngle;
    private float updateAngle;
    public static bool IsWalking;
    public static bool IsRunning;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float G_radius;
    public LayerMask G_checklayer;

    
    [Header("Camera")]
    public Camera cam;

    [Header("Attack")]
    public Transform Attackpoint;
    public float A_radius;
    public static bool isAttack;

    [Header("TimeAttack")]
    public float TimeAttack;
    private float T_Attack;



    private Rigidbody rb;
    private Animator anim;
    private Player input;

    private void Awake()
    {
        input = new Player();

        //movimento do jogador
        input.PlayerController.Moviment.performed += x => Move(x.ReadValue<Vector2>());

        //rotação do jogador
        input.PlayerController.RotationGamePad.performed += x => RotationGamepad(x.ReadValue<Vector2>());
        //input.PlayerController.RotationMouse.performed += x => RotationMouse(x.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        input.PlayerController.Enable();
    }
    private void OnDisable()
    {
        input.PlayerController.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
   

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        
      

        // MoveInput();
        // Controller();

        // RotationCharacter();
        //  Setlight();


    }
    public void Move(Vector2 _move)
    {
        rb.velocity = new Vector3(_move.x, 0, _move.y) * PlayerStats.instance.speed;
    }
    public void RotationGamepad(Vector2 _rot)
    {

      

        updateAngle = Mathf.Atan2(_rot.y, _rot.x) * Mathf.Rad2Deg - 90f;

        transform.eulerAngles = new Vector3(0, updateAngle, 0);
        
       
    }
    public void RotationMouse(Vector2 _mouse)
    {
        _mouse = cam.ScreenToWorldPoint(_mouse);

        Debug.Log(_mouse);
        Ray cameraray = cam.ScreenPointToRay(_mouse);

        Plane groundplane = new Plane(Vector3.up, Vector3.zero);

        float raylenght;



        if (Physics.Raycast(cameraray, out RaycastHit raycast, Mathf.Infinity, layerHit))
        {

            Vector3 pointToLook = cameraray.GetPoint(raycast.distance);

            Debug.DrawLine(cameraray.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    private void FixedUpdate()
    {
        Gravity();
    }
    void Controller()
    {
        

        if (Input.GetKey(KeyCode.Mouse0) && PlayerStats.instance.energy > 0 || Input.GetAxis("Fire1") == 1 && PlayerStats.instance.energy > 0)
        {
            
            anim.SetBool("IsAttaking", true);
            isAttack = true;
           
        }

        
    }
    private void Gravity()
    {
        if (GroundCheck() && move.y < 0) 
        {
            move.y = -2f;
        }
        else
        {
            move.y = -30f * Time.fixedDeltaTime;
        }
      
    }
    private bool GroundCheck()
    {
        bool isgrounded = Physics.CheckSphere(groundCheck.position, G_radius, G_checklayer);

        if (isgrounded)
        {
            return true;
        }

        return false;
    }
    void MoveInput()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        float velocity;

        
        

        if (Input.GetKey(KeyCode.LeftShift) && PlayerStats.instance.energy > 0 || Input.GetAxis("Run") >=1 && PlayerStats.instance.energy > 0)
        {
            velocity = PlayerStats.instance.speed +PlayerStats.instance.speedrun;
            
            IsRunning = true;

            
        }
        else
        {
            velocity = PlayerStats.instance.speed;

            IsRunning = false;

        }
        
        
        

        if (move.x != 0 && !IsRunning || move.y != 0 && !IsRunning)
        {
            IsWalking = true;

        }
        else
        {
            IsWalking = false;
        }

        rb.velocity = move * velocity;

        anim.SetBool("IsRunning", IsRunning);
        anim.SetBool("IsMoving", IsWalking);
    }
    private void Setlight()
    {
        if (!TGSky.isNight)
        {
            
            light.SetActive(false);

        }
        else
        {
            
            light.SetActive(true);
        }
    }
    void RotationCharacter()
    {
        


        //rotação com mouse



      

     

    }
    public void TakeDamage(int dmg)
    {
        PlayerStats.instance.life -= dmg;
    }
    public void Attack()
    {
        PlayerStats.instance.energy--;
        
        Collider[] hitInfo = Physics.OverlapSphere(Attackpoint.position, A_radius);
        
        foreach (Collider hit in hitInfo)
        {
            if (hit.CompareTag("Object"))
            {
                hit.gameObject.GetComponent<ObjectsController>().TakeDamage(1);

            }
            if (hit.CompareTag("Enemy"))
            {
                hit.gameObject.GetComponent<EnemyController>().TakeDamage(PlayerStats.instance.damage);
            }
            if (hit.CompareTag("Animal"))
            {
                hit.gameObject.GetComponent<AnimalController>().TakeDamage(PlayerStats.instance.damage);
            }

        }

        anim.SetBool("IsAttaking", false);
    }
    public void AttackFalse()
    {
        anim.SetBool("IsAttaking", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpoint.position,A_radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject light;

    [Header("Input")]
    public Vector3 _rotMouse;
    private Vector2 _rotStick;
    private Vector3 move;
    public float rotationspeed;
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
   
    // Start is called before the first frame update
    void Start()
    {
   

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       


        MoveInput();
        Controller();
       
        RotationCharacter();
        Setlight();


    }
    private void FixedUpdate()
    {
        Gravity();
    }
    void Controller()
    {
        float i = Input.GetAxis("Fire1");

        if (Input.GetKey(KeyCode.Mouse0) && PlayerStats.instance.energy > 0 || i == 1 && PlayerStats.instance.energy > 0)
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
        move.z = Input.GetAxisRaw("Vertical");

        float velocity=0;

        
        

        if (Input.GetKey(KeyCode.LeftShift) && PlayerStats.instance.energy > 0 || Input.GetAxis("Run") >=1 && PlayerStats.instance.energy > 0)
        {
            velocity = PlayerStats.instance.run;
            
            IsRunning = true;
          

        }
        else
        {
            velocity = PlayerStats.instance.speed;

            IsRunning = false;

        }
        
        
        

        if (move.x != 0 && !IsRunning || move.z != 0 && !IsRunning)
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
        //rotação com controle

        _rotStick.x = -Input.GetAxisRaw("Stick Right H");
        _rotStick.y = -Input.GetAxisRaw("Stick Right V");

        if (_rotStick.x >= 0.2f || _rotStick.x <= -0.2f || _rotStick.y >= 0.2f || _rotStick.y <= -0.2f)
        {

            lastAngle = updateAngle;
        }

        if (GameController.usingController)
        {
            updateAngle = Mathf.Atan2(_rotStick.y, _rotStick.x) * Mathf.Rad2Deg - 90f;

            transform.eulerAngles = new Vector3(0, lastAngle, 0);

        }

        //rotação com mouse

        if (!GameController.usingController)
        {
            Ray cameraray = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundplane = new Plane(Vector3.up, Vector3.zero);
            float raylenght;
            if (groundplane.Raycast(cameraray, out raylenght))
            {
                Vector3 pointToLook = cameraray.GetPoint(raylenght);
                Debug.DrawLine(cameraray.origin, pointToLook, Color.blue);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                
                
            }
        }


      

     

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

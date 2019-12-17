using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Camera Settings")]
    public Transform CameraPlayer;
    public float sensibility;
    public float minAngle;
    public float maxAngle;

    float yRotate = 0f;
    float xRotate = 0f;

    [Header("Movement Settings")]

    public CharacterController controller;
    public float velocidadbase;
    public float velocidad;
    public float run;
    public float gravity;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    public float fuerzaSalto;
    bool isGrounded;


    [Header("Interaction Settings")]
    public int noise;
    public LayerMask zombieLayer;

    PlayerStats ps;
    Rigidbody rg;
    Transform tr;
    Vector3 velocity;



    // Start is called before the first frame update
    void Start()
    {
        //gravity = -9.81f;
        groundDistance = 0.3f;
        velocidad = velocidadbase;
        //rg = GetComponent<Rigidbody>();
        tr = this.transform;
        ps = GetComponent<PlayerStats>();
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward) * 1f, Color.blue);


        camerarotation();        
        movement();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

    }

    void movement()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            velocidad = velocidad / 2;
            ps.noise = ps.baseNoise / 2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            velocidad = velocidadbase;
            ps.noise = ps.baseNoise;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * velocidad * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(fuerzaSalto * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        //Vector3 sp = rg.velocity;

        //float deltaX = Input.GetAxis("Horizontal");
        //float deltaZ = Input.GetAxis("Vertical");
        //float deltaT = Time.deltaTime;

        //Vector3 side = velocidad * deltaX * deltaT * tr.right;
        //Vector3 forward = velocidad * deltaZ * deltaT * tr.forward;

        //Vector3 endSpeed = side + forward;

        //endSpeed.y = sp.y;

        //rg.velocity = endSpeed;

    }

    void camerarotation()
    {

        xRotate += Input.GetAxis("Mouse X");
        yRotate = Mathf.Min(minAngle, Mathf.Max(maxAngle, yRotate + Input.GetAxis("Mouse Y")));
        gameObject.transform.localRotation = Quaternion.Euler(0, xRotate, 0);
        CameraPlayer.transform.localRotation = Quaternion.Euler(-yRotate, 0, 0);

    }

    void Fire()
    {
        RaycastHit hit;

        

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            Debug.Log("PLAYER HIT TAG:"+hit.transform.gameObject.tag);

            if (hit.collider.GetType() == typeof(BoxCollider) && hit.transform.gameObject.tag.Equals("zombie"))
            {
                Debug.Log("PLAYER // HITTING ZOMBIE");
                hit.transform.gameObject.GetComponent<ZombieStats>().GetDamage(ps.attackDamage);
            }



        }

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Drink(float quantity)
    {
        ps.addThirst(quantity);
    }


}

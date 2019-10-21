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

    public float velocidadbase;
    public float velocidad;
    public float run;
    public float fuerzaSalto;
    public bool isGrounded;

    public Transform attackSphereTr;
    public LayerMask zombieLayer;

    PlayerStats ps;
    Rigidbody rg;
    Transform tr;



    // Start is called before the first frame update
    void Start()
    {
        velocidad = velocidadbase;
        rg = GetComponent<Rigidbody>();
        tr = this.transform;
        ps = GetComponent<PlayerStats>();

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

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rg.AddForce(tr.up * fuerzaSalto);
            isGrounded = false;
        }

        Vector3 sp = rg.velocity;

        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        float deltaT = Time.deltaTime;

        Vector3 side = velocidad * deltaX * deltaT * tr.right;
        Vector3 forward = velocidad * deltaZ * deltaT * tr.forward;

        Vector3 endSpeed = side + forward;

        endSpeed.y = sp.y;

        rg.velocity = endSpeed;

    }

    void camerarotation()
    {

        xRotate += Input.GetAxis("Mouse X");
        yRotate = Mathf.Min(50, Mathf.Max(-50, yRotate + Input.GetAxis("Mouse Y")));
        gameObject.transform.localRotation = Quaternion.Euler(0, xRotate, 0);
        CameraPlayer.transform.localRotation = Quaternion.Euler(-yRotate, 0, 0);

    }

    void Fire()
    {
        RaycastHit hit;

        

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward), out hit, 1f))
        {
            Debug.Log(hit.distance);
            Debug.Log("HITTING SOMETHING");
            Debug.Log(hit.transform.gameObject.tag);

            if (hit.collider.GetType() == typeof(BoxCollider) && hit.transform.gameObject.tag.Equals("zombie"))
            {
                Debug.Log("HITTING --------BOX");
                hit.transform.gameObject.GetComponent<ZombieStats>().GetDamage(ps.attackDamage);
            }



        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "floor")
        {
            isGrounded = true;
            
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }




}

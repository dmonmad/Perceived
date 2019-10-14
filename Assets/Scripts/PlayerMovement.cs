using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
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

    Rigidbody rg;
    Transform tr;



    // Start is called before the first frame update
    void Start()
    {
        velocidad = velocidadbase;
        rg = GetComponent<Rigidbody>();
        tr = this.transform;

    }

    private void Update()
    {
        camerarotation();        
        movement();
               
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

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "floor")
        {
            isGrounded = true;
            
        }
    }


}

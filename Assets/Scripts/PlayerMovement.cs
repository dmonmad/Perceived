using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Camera CameraPlayer;
    public float sensibility;

    float yRotate;
    float xRotate;

    public float velocidadbase;
    public float velocidad;
    public float run;

    public float fuerzaSalto;
    public bool isGrounded;

    private Rigidbody selfRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        velocidad = velocidadbase;
        selfRigidbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        camerarotation();

        movement();

    }


    void camerarotation()
    {

        xRotate += Input.GetAxis("Mouse X");
        yRotate = Mathf.Min(50, Mathf.Max(-50, yRotate + Input.GetAxis("Mouse Y")));
        gameObject.transform.localRotation = Quaternion.Euler(0, xRotate, 0);
        CameraPlayer.transform.localRotation = Quaternion.Euler(-yRotate, 0, 0);

    }

    void movement()
    {
        Vector3 direction = gameObject.transform.position;

        //if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        //{
        //    Debug.Log("Pressing both W and D/A");
        //    velocidad = velocidad / 2;
        //}
        //else
        //{
        //    velocidad = velocidadbase;
        //}

        //if ()
        //{
        //    Debug.Log("Pressing both S and D/A");
        //    velocidad = velocidad / 2;
        //}
        //else
        //{
        //    velocidad = velocidadbase;
        //}



        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.forward * Time.deltaTime * velocidad;

        }

        if (Input.GetKey(KeyCode.D))
        { 
            direction += transform.right * Time.deltaTime * velocidad;

        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += -transform.right * Time.deltaTime * velocidad;

        }

        if (Input.GetKey(KeyCode.S))
        {

            direction += -transform.forward * Time.deltaTime * velocidad;

        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (isGrounded))
        {
            selfRigidbody.AddForce(0, fuerzaSalto, 0);
            isGrounded = false;
        }


        selfRigidbody.MovePosition(direction);
        

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "floor")
        {
            isGrounded = true;
            
        }
    }


}

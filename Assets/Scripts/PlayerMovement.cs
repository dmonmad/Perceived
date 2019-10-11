using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Camera CameraPlayer;
    private float sensibility = 1.5f;

    float yRotate;
    float xRotate;

    public float velocidad = 1f;
    public float run;

    public float fuerzaSalto = 150f;
    public bool isGrounded;

    private Rigidbody selfRigidbody;


    // Start is called before the first frame update
    void Start()
    {

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
        Vector3 direction = new Vector3(0,0,0);

        if (Input.GetKey(KeyCode.W))
        {
            direction += transform.position + (transform.forward * Time.deltaTime * velocidad);

        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += transform.position + (transform.right * Time.deltaTime * velocidad);

        }

        if (Input.GetKey(KeyCode.A))
        {
            direction += transform.position + (-transform.right * Time.deltaTime * velocidad);

        }

        if (Input.GetKey(KeyCode.S))
        {

            direction += transform.position + (-transform.forward * Time.deltaTime * velocidad);

        }

        if (Input.GetKey()
        {
            selfRigidbody.MovePosition(direction);
        }

    }
}

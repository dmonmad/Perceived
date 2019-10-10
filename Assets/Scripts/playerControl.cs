using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public GameObject camPlayerScript;
    public GameObject Target;
    Camera cameraPlayerCam;

    private float sensibility = 1.5f;

    float v;
    float h;

    private float velocidad = 0.01f;
    private float run;

    public float fuerzaSalto = 150f;
    public bool isGrounded;

    Ray rayAim;
    private Rigidbody selfRigidbody;
    private RaycastHit hit;




    void Start()
    {

        selfRigidbody = GetComponent<Rigidbody>();

        cameraPlayerCam = camPlayerScript.GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {

        h = sensibility * Input.GetAxis("Mouse X");
        v = sensibility * Input.GetAxis("Mouse Y");

        Target.transform.Rotate(0, h, 0);
        camPlayerScript.transform.Rotate(-v, 0, 0);

        rayAim = cameraPlayerCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

        if (Physics.Raycast(rayAim, out hit, 15f))
        { }
            /*if (hit.collider.CompareTag("NPC"))
            {
                Debug.DrawLine(rayAim.origin, rayAim.direction * 10 - rayAim.origin, Color.green);
                Debug.Log("Did Hit");
                for (i = 0; i < 1; i++) { 
                Instantiate(myPrefab, hit.point, Quaternion.identity);
            }
            }
            else
            {
                Debug.DrawLine(rayAim.origin, rayAim.direction * 10, Color.yellow);
            }
        }
        else
        {
                    Debug.DrawLine(rayAim.origin, rayAim.direction * 10, Color.red);
        }*/
 



        if (Input.GetKey(KeyCode.W))
        {
            Target.transform.Translate(0, 0, velocidad * run);

        }

        if (Input.GetKey(KeyCode.D))
        {
            Target.transform.Translate(velocidad * run, 0, 0);

        }

        if (Input.GetKey(KeyCode.A))
        {
            Target.transform.Translate(-velocidad * run, 0, 0);

        }

        if (Input.GetKey(KeyCode.S))
        {
            Target.transform.Translate(0, 0, -velocidad * run);

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 2f;
        }
        else
        {
            run = 1f;
        }

        if ((Input.GetKeyDown(KeyCode.Space)) && (isGrounded))
        {
            selfRigidbody.AddForce(0, fuerzaSalto, 0);
        }

        
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == "floor")
        {
            isGrounded = true;
            Debug.Log(collision.collider.tag);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            isGrounded = false;
            Debug.Log("En el aire");
        }
    }
}
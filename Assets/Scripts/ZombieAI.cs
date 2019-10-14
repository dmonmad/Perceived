using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{

    public SphereCollider hearArea;
    public float sightRadius;
    public float noisetrigger;

    public float sightDistance;
    public bool outofradius;

    public Player target;
    private Rigidbody rb;
    public float zombieSpeed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hearArea.radius = sightRadius;
        outofradius = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 lastPosition;

        RaycastHit hit;

        bool targetBehindObject = Physics.Raycast(transform.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), out hit, sightDistance);



        if (target)
        {
            movetowards();
            
        }



        if (outofradius || hit.collider.gameObject.tag!="zombie")
        {
            if(target && Vector3.Distance(target.transform.position, transform.position) > sightDistance)
            {
                
                target = null;
                
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            target = other.gameObject.GetComponent<Player>();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "player")
        {
            outofradius = true;
        }

    }

    void movetowards()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);

        Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

        rb.MovePosition(direction);
    }


    private void OnDrawGizmos()
    {
        
    }
}

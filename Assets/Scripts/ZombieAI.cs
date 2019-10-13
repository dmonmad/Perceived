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
    void Update()
    {
        if (target)
        {
            movetowards();
            
        }

        if (outofradius)
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

        Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.deltaTime;

        rb.MovePosition(direction);
    }
}

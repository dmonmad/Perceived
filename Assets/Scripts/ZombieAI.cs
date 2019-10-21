using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public bool isAlive;

    public SphereCollider hearArea;
    public float sightRadius;
    public float noisetrigger;

    public float sightDistance;

    public PlayerStats target;
    private Rigidbody rb;
    public float zombieSpeed;
    public float rotationSpeed;

    private Animator anim;

    Vector3 actualplayerpos;
    Vector3 playerlastpos;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        playerlastpos = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        hearArea.radius = sightRadius;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }

        RaycastHit hit;
        bool targetBehindObject = Physics.Raycast(transform.position, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z), out hit, sightDistance);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject)
            {
                Debug.DrawRay(transform.position, transform.forward * sightDistance, Color.red);
            }
        }



        if (target && Vector3.Distance(target.transform.position, transform.position) < sightDistance)
        {
            playerlastpos = target.transform.position;
            moveTowardsPlayer(target.transform.position);
            
        }
        else if (!target && playerlastpos != Vector3.zero)
        {
            target = null;
            moveTowardsLastPos(playerlastpos);
        }
        else
        {
            if (anim.GetBool("isWalking"))
            {
                anim.SetBool("isWalking", false);
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isAlive)
        {
            return;
        }

        if (other.gameObject.tag == "player")
        {
            target = other.gameObject.GetComponent<PlayerStats>();
        }


    }

    void moveTowardsPlayer(Vector3 playerpos)
    {
        if (!anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", true);
        }

        actualplayerpos = playerpos;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(actualplayerpos - transform.position), rotationSpeed * Time.deltaTime);

        Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

        rb.MovePosition(direction);
    }

    void moveTowardsLastPos(Vector3 lastpos)
    {

        if (!anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", true);
        }

        actualplayerpos = lastpos;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lastpos - transform.position), rotationSpeed * Time.deltaTime);

        Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

        rb.MovePosition(direction);

        
    }

    public void Die()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.rotation = new Quaternion(-90, transform.rotation.y, transform.rotation.z, 0);
        isAlive = false;
        
    }




}

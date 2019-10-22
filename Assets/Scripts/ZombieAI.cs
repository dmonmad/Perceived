using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("Control Variables")]
    ZombieStats zs;
    public bool isAlive;
    public bool isAttacking;

    [Header("Detection variables")]

    public SphereCollider hearArea;
    public float sightRadius;
    public float sightRadiusWithTarget;
    public float noisetrigger;
    public float distanceToAttack;
    public float sightDistance;


    [Header("Zombie Variables")]
    public PlayerStats target;
    private Rigidbody rb;
    public float zombieSpeed;
    public float rotationSpeed;

    private Animator anim;
    Vector3 playerlastpos;

    // Start is called before the first frame update
    void Start()
    {
        zs = GetComponent<ZombieStats>();
        isAlive = true;
        isAttacking = false;
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

        

        if (!isAttacking)
        {
            if (target)
            {
                CheckIfBehindObject();

                float distance = Vector3.Distance(target.transform.position, transform.position);

                if (distance < sightDistance)
                {
                    Debug.Log("SEEKING PLAYER");
                    playerlastpos = target.transform.position;
                    moveTowardsPlayer(target.transform.position, distance);
                }

                if (distance > sightDistance)
                {
                    Debug.LogWarning("TARGET IS OUT. TARGET NULL");
                    target = null;
                }
            }
            else if (!target && playerlastpos != Vector3.zero)
            {
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
    }

    private bool CheckIfBehindObject()
    {
        bool targetBehindObject = false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward * sightDistance, out hit, sightDistance))
        {
            if (hit.collider.gameObject.tag != "zombie" || hit.collider.gameObject.tag != "player")
            {
                targetBehindObject = true;
            }
        }
        return targetBehindObject;
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
            hearArea.radius = sightRadiusWithTarget;
        }
    }

    void moveTowardsPlayer(Vector3 playerpos, float distance)
    {
        Debug.Log(distance);
        if (distance < distanceToAttack)
        {
            Attack();
            return;
        }

        if (!anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", true);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerpos - transform.position), rotationSpeed * Time.deltaTime);

        Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

        rb.MovePosition(direction);
    }

    void moveTowardsLastPos(Vector3 lastpos)
    {

        if (Vector3.Distance(lastpos, gameObject.transform.position) < distanceToAttack)
        {
            playerlastpos = Vector3.zero;
            hearArea.radius = sightRadius;
            return;
        }

        if (!anim.GetBool("isWalking"))
        {
            anim.SetBool("isWalking", true);
        }

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

    public void Attack()
    {
        isAttacking = true;
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", true);
    }

    public void AnimAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            Debug.Log("ZOMBIE HIT TAG:"+hit.transform.gameObject.tag);

            if (hit.collider.GetType() == typeof(CapsuleCollider) && hit.transform.gameObject.tag.Equals("player"))
            {
                Debug.Log("HITTING PLAYER");
                hit.transform.gameObject.GetComponent<PlayerStats>().GetDamage(zs.attackDamage);
            }

        }
    }


}

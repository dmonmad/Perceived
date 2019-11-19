using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    bool active;

    [Header("Control Variables")]
    ZombieStats zs;
    public bool isAlive;
    public bool isAttacking;

    [Header("Detection variables")]

    public SphereCollider hearArea;
    public float hearRadius;
    public float hearRadiusWithTarget;
    public float noisetrigger;
    public float sightDistance;
    public LayerMask PlayerMask;


    [Header("Zombie Variables")]
    public PlayerStats target;
    private Rigidbody rb;
    public float zombieSpeed;
    public float rotationSpeed;

    [Header("Movement Variables")]
    NavMeshAgent agent;

    private Animator anim;
    Vector3 playerlastpos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetActive());
        zs = GetComponent<ZombieStats>();
        isAlive = true;
        isAttacking = false;
        playerlastpos = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        hearArea.radius = hearRadius;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            if (!isAlive)
            {
                return;
            }

            //if (!isAttacking)
            //{
            //    if (target)
            //    {

            //        float distance = Vector3.Distance(target.transform.position, transform.position);

            //        if (distance < sightDistance)
            //        {
            //            if (CheckIfBehindObject())
            //            {
            //                playerlastpos = target.transform.position;
            //                agent.SetDestination(target.transform.position);
            //                target = null;
            //            }
            //            else
            //            {
            //                Debug.Log("SEEKING PLAYER");
            //                playerlastpos = target.transform.position;
            //                agent.SetDestination(target.transform.position);
            //            }

            //        }

            //        if (distance > sightDistance)
            //        {
            //            Debug.LogWarning("TARGET IS OUT. TARGET NULL");
            //            target = null;

            //        }
            //    }
            //    else if (!target && playerlastpos != Vector3.zero)
            //    {
            //        //moveTowardsLastPos(playerlastpos);
            //    }
            //    else
            //    {
            //        if (anim.GetBool("isWalking"))
            //        {
            //            anim.SetBool("isWalking", false);
            //        }
            //    }
            //}



            if (target)
            {

                playerlastpos = target.transform.position;


                if (!isAttacking)
                {
                    if (isTargetBehindObject())
                    {
                        target = null;
                        hearArea.radius = hearRadius;
                        GoToLastPosition();
                    }
                    else
                    {
                        SeekPlayer();
                    }
                }
                else
                {

                }

            }
            else
            {
                agent.isStopped = true;
                agent.ResetPath();
            }


        }
    }

    private void GoToLastPosition()
    {
        if(playerlastpos != null)
        {
            agent.SetDestination(playerlastpos);
        }


    }

    private void SeekPlayer()
    {

        agent.SetDestination(target.transform.position);

        if (agent.isStopped && Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance)
        {
            Attack();
        }

    }

    private bool isTargetBehindObject()
    {
        bool targetBehindObject = false;

        RaycastHit hit;

        Debug.DrawRay(transform.position, (target.transform.position - transform.position));

        if (Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit, sightDistance))
        {
            Debug.Log("HITTING SOMETHING" + hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag != "player" && hit.collider.gameObject.tag != "zombie")
            {
                Debug.Log("Player está detrás de " + hit.collider.gameObject.name);
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
            hearArea.radius = hearRadiusWithTarget;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isAlive)
        {
            return;
        }

        if(other.gameObject.tag == "player")
        {
            if(target != null)
            {
                target = null;
                hearArea.radius = hearRadius; 
            }
        }
    }

    //void moveTowardsPlayer(Vector3 playerpos, float distance)
    //{
    //    Debug.Log(distance);
    //    if (distance < distanceToAttack)
    //    {
    //        Attack();
    //        return;
    //    }

    //    if (!anim.GetBool("isWalking"))
    //    {
    //        anim.SetBool("isWalking", true);
    //    }

    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerpos - transform.position), rotationSpeed * Time.deltaTime);

    //    Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

    //    agent.SetDestination(direction);
    //}

    //void moveTowardsLastPos(Vector3 lastpos)
    //{

    //    if (Vector3.Distance(lastpos, gameObject.transform.position) < distanceToAttack)
    //    {
    //        playerlastpos = Vector3.zero;
    //        hearArea.radius = sightRadius;
    //        return;
    //    }

    //    if (!anim.GetBool("isWalking"))
    //    {
    //        anim.SetBool("isWalking", true);
    //    }

    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lastpos - transform.position), rotationSpeed * Time.deltaTime);

    //    Vector3 direction = transform.position += transform.forward * zombieSpeed * Time.fixedDeltaTime;

    //    agent.SetDestination(direction);
    //}

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

    private IEnumerator SetActive()
    {
        yield return new WaitForSeconds(3);
        active = true;
    }


}

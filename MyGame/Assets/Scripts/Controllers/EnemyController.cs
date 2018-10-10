using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    CharacterCombat combat;
    static Animator anim;
    Transform target;
    NavMeshAgent agent;
    public Slider healthBar;

    // Use this for initialization
    void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if (healthBar.value <= 0)
            return;
       
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.stoppingDistance = 1.75F;
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
       
            if (distance < agent.stoppingDistance)
            {
                anim.SetTrigger("isAttacking");
                anim.SetBool("isRunning", false);
                FaceTarget();
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isRunning", false);


        }
    }


    //É colidido pelo Player e perde vida
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        healthBar.value -= 20;
        anim.SetTrigger("isTakingDamage");
        anim.SetBool("isRunning", false);
        if (healthBar.value <= 0)
        {
            anim.SetTrigger("isDead");
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    CharacterCombat combat;
    static Animator anim;
    detectHit hitted;
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
        {
            return;
        }


        if ( distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            agent.stoppingDistance = 1.75F;
            //agent.transform.Translate(0,0,0.05F);
        
    

            if (distance < agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                combat.Attack(targetStats);
                    
                }

                //o inimigo está a ser atacado animation
                //if (hitted.collider.Equals(gameObject)) { 
                //    anim.SetBool("isTakingDamage", true);
                //    anim.SetBool("isRunning", false);
                //    anim.SetBool("isAttacking", false);
                //    anim.SetBool("isDead", false);

                //}
                FaceTarget();

            }
        }
     

    }


    //É colidido pelo Player e perde vida
    private void OnTriggerEnter(Collider other)
    {       
        Debug.Log("Hit");
        healthBar.value -= 20;
        if (healthBar.value <= 0)
        {
            anim.SetBool("isDead", true);
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

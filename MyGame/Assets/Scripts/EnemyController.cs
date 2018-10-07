using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public float lookRadius = 10f;
    public int life = 20;
    static Animator anim;

    Transform target;
    NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    public EnemyController(int Life)
    {
        


    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(target.position, transform.position);

        if( distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            anim.SetBool("isRunning", true);
            anim.SetBool("isAttacking", false);
        }

        if( distance < 3)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isAttacking", true);
        }
		
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

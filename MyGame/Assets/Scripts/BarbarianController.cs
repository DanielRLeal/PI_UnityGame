using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianController : MonoBehaviour
{
    public LayerMask movementMask;
    static Animator anim;
    public float speed = 10.0F;
    public int maxRange = 5;
    public float rotationSpeed = 100.0F;


    // Use this for initialization
    void Start()
    {
             anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("isRoundKicking");
        }

        if (translation != 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("isJumping");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {

            Destroy(other.gameObject);

            Debug.Log("Collide");
        }
    }

}


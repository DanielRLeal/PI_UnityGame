using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BarbarianController : MonoBehaviour
{
    private bool hasCollided = false;
    public LayerMask movementMask;
    static Animator anim;
    public float speed = 10.0F;
    public int maxRange = 5;
    public float rotationSpeed = 100.0F;
    public Collider other;
    public Slider healthBar;


    // Use this for initialization
    void Start()
    {
             anim = GetComponent<Animator>();
    }
      

    // Update is called once per frame
    void Update()
    {
        if (healthBar.value <= 0)
        {
            return;
        }

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);


      
     
        if (Input.GetKeyDown(KeyCode.T) && hasCollided==true)
        {
            Destroy(other.gameObject);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            int r = UnityEngine.Random.Range(0, 2);
            if (r.Equals(0))
            {
                anim.SetTrigger("isRoundKicking");
                anim.SetBool("isIdle", false);

            }
            if (r.Equals(1))
            {
                anim.SetTrigger("isLeftUpper");
                anim.SetBool("isIdle", false);
            }
            if (r.Equals(2))
            {
                anim.SetTrigger("isRightHook");
                anim.SetBool("isIdle", false);
            }
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
        this.other = other;
        if (other.gameObject.CompareTag("Item"))
        {

            hasCollided = true;
            Debug.Log("Collide");
        }

        healthBar.value -= 20;
        if (healthBar.value <= 0)
        {
            anim.SetTrigger("isDead");
        }

    }

    void OnTriggerExit(Collider other )
    {
        hasCollided = false;

    }

}


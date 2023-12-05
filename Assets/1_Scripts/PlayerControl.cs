using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float jPower;

    bool jDown;
    bool fDown;

    bool isJump;
    public bool isFireReady;

    Rigidbody rigid;
    Animator anim;

    [SerializeField] Sword sword;
    float fireDelay;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Jump();
        Attack();
    }

    void GetInput()
    {
        //hAxis = Input.GetAxisRaw("Horizontal");
        //vAxis = Input.GetAxisRaw("Vertical");
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire1");
    }

    void Jump()
    {
        if (jDown && !isJump)
        {
            rigid.AddForce(Vector3.up * jPower, ForceMode.Impulse);
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = sword.rate < fireDelay;

        if(fDown && isFireReady)
        {
            sword.Use();
            anim.SetTrigger("doAttack");
            fireDelay = 0;

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float jPower;

    bool jDown;
    bool fDown;

    bool isJump;
    public bool isFireReady;

    public bool cantMove;

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
        Attack();
    }

    void GetInput()
    {
        fDown = Input.GetButtonDown("Fire1");
    }

    void OnJump()
    {
        if (!isJump)
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

        if (fDown && isFireReady)
        {
            sword.Use();
            anim.SetTrigger("doAttack");
            fireDelay = 0;
            cantMove = true;
        }
        else
            cantMove = false;

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

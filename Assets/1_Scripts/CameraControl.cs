using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;

    Vector3 moveDir;

    bool rDown;

    [SerializeField] float speed;
    [SerializeField] float rSpeed;

    [SerializeField] Animator anim;

    void Update()
    {
        LookAround();
        Move();
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        bool isMove = moveInput.magnitude != 0;
        anim.SetBool("isWalk", isMove);

        if (!characterBody.gameObject.GetComponent<PlayerControl>().isFireReady)
        {
            isMove = false;
            moveDir=Vector3.zero;
        }
        if (isMove)
        {
            rDown = Input.GetButton("Run");
            anim.SetBool("isRun", rDown);

            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
   
        }
        transform.position += moveDir * speed * (rDown ? rSpeed : 1f) * Time.deltaTime;
    }

    void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform characterBody;
    [SerializeField] Transform cameraArm;

    Vector2 inputVec;
    Vector3 moveDir;
    Vector3 lookForward;
    Vector3 lookRight;
    bool isMove;

    Vector2 mouseDelta;

    bool rDown;

    [SerializeField] float speed;
    [SerializeField] float rSpeed;

    [SerializeField] Animator anim;

    private void Awake()
    {
        Application.targetFrameRate = 30;   // 프레임 고정
        mouseDelta = Vector2.zero;
    }
    void FixedUpdate()
    {
        LookAround();
        Move();
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
        Debug.Log("다시 걸어");
    }

    void Move()
    {
        isMove = inputVec.magnitude != 0;
        anim.SetBool("isWalk", isMove);

        if (!characterBody.gameObject.GetComponent<PlayerControl>().isFireReady)
        {
            Stop();
        }

        if (isMove)
        {
            rDown = Input.GetButton("Run");
            anim.SetBool("isRun", rDown);

            lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;

            moveDir = lookForward * inputVec.y + lookRight * inputVec.x;
            characterBody.forward = moveDir;


            transform.position += moveDir * speed * (rDown ? rSpeed : 1f) * Time.deltaTime;

        }

        
    }//  그렇다면 애니메이션 프레임에 이벤트를 걸어서 공격 끝나면 다시 걷게 ㅎ해볼까.

    void Stop()
    {
        Debug.Log("멈");
        isMove = false;
      //  inputVec = Vector3.zero;
    }

    void LookAround()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * 2.0f); // 마우스 움직인 수치
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // 카메라의 회전값을 오일러 각으로 변환
        float x = camAngle.x - mouseDelta.y; // 마우스 수직 움직임(마우스 수직 움직임으로 카메라 상하 움직임 제어)

        // x 회전 제한
        if (x < 180f) // 위로 회전
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

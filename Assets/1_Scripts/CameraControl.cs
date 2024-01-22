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
        Application.targetFrameRate = 30;   // ������ ����
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
        Debug.Log("�ٽ� �ɾ�");
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

        
    }//  �׷��ٸ� �ִϸ��̼� �����ӿ� �̺�Ʈ�� �ɾ ���� ������ �ٽ� �Ȱ� ���غ���.

    void Stop()
    {
        Debug.Log("��");
        isMove = false;
      //  inputVec = Vector3.zero;
    }

    void LookAround()
    {
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * 2.0f); // ���콺 ������ ��ġ
        Vector3 camAngle = cameraArm.rotation.eulerAngles; // ī�޶��� ȸ������ ���Ϸ� ������ ��ȯ
        float x = camAngle.x - mouseDelta.y; // ���콺 ���� ������(���콺 ���� ���������� ī�޶� ���� ������ ����)

        // x ȸ�� ����
        if (x < 180f) // ���� ȸ��
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

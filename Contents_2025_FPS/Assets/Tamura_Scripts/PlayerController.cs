using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  �v���C���[�̈ړ���S������(FPS�Ȃ̂ŃJ�����ɂ���)
public class PlayerController : MonoBehaviour
{
    // ------------------------------------------�ϐ�---------------------------------

    [SerializeField] Transform cameraTransform; // ���Ⴊ�ލۂ̃J�����ړ��Ɏg��
    [SerializeField] Vector3 standCenter = new Vector3(0, 1.0f, 0); // �����Ă��鎞�̔���̒��S
    [SerializeField] Vector3 crouchCenter = new Vector3(0, 0.5f, 0); // ���Ⴊ��ł���Ƃ��̔���̒��S
    [SerializeField] float moveSpeed = 1.0f; // �ʏ�̈ړ����x
    [SerializeField] float standHeight = 1.7f; // �����Ă��鎞�̍���
    [SerializeField] float crouchMoveSpeed = 1.0f; //�@���Ⴊ�񂾎��̈ړ����x
    [SerializeField] float crouchHeight = 1.0f; // ���Ⴊ�񂾎��̍���
    [SerializeField] float crouchSpeed = 1.0f;�@//�@���Ⴊ�ނƂ��̃X�s�[�h
    [SerializeField] float dashMoveSpeed = 1.0f; // �_�b�V�����̈ړ����x
    [SerializeField] float jumpHeight = 1.0f; // �W�����v�̍���
    [SerializeField] float standCameraY = 1.7f; // �ʏ�̃J�����̍���
    [SerializeField] float crouchCameraY = 0.8f; // ���Ⴊ�񂾎��̃J�����̍���
    CapsuleCollider capsuleCollider; // ���Ⴊ�݂Ɏg��
    Rigidbody rb; // �ړ��Ɏg��
    Vector3 moveDir = Vector3.zero; // �ړ�����
    Vector3 moveValue = Vector3.zero; // �ړ������
    Vector3 defaultScale = Vector3.one; // �ʏ��Ԃ̑傫��(���Ⴊ�ݎ��ɎQ��)
    float currentSpeed = 0.0f; // ���݂̃X�s�[�h���擾
    bool isJump = false; // �W�����v�p�̃t���O
    bool isCrouch = false; // ���Ⴊ�ݗp�̃t���O

    // ------------------------------------------�֐�---------------------------------------


    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody���擾
        capsuleCollider = rb.GetComponent<CapsuleCollider>(); // �J�v�Z���R���C�_�[���擾�@�@
    }


    // ���͏�����Update
    private void Update()
    {
        InputMove();
        InputJump();
    }

    //����������FixedUpdate�ŕ�����
    void FixedUpdate()
    {
        CalculateMove();
    }



    //  �ړ��Ɋւ�����͂��󂯕t����
    void InputMove()
    {
        moveDir = Vector3.zero; // �ړ�����
        moveValue = Vector3.zero; // �ړ������

        if (this.gameObject != null)
        {
            // �e��Ԃ�ݒ� (�ʏ�ړ�, �_�b�V��, ���Ⴊ��) 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = dashMoveSpeed; // �_�b�V���p�̃X�s�[�h
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouch = true;
                currentSpeed = crouchMoveSpeed; // ���Ⴊ�ݗp�̃X�s�[�h
                // �e�ϐ����J�v�Z���R���C�_�[�ɓK�p
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = crouchCenter;
            }
            else
            {
                isCrouch = false; // ���Ⴊ��ł��Ȃ�����false
                currentSpeed = moveSpeed; // �ʏ�̃X�s�[�h
                // �e�ϐ����J�v�Z���R���C�_�[�ɓK�p
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }

            CrouchCamera();

            // �ړ�������ݒ�
            float dirX = Input.GetAxisRaw("Horizontal");
            float dirZ = Input.GetAxisRaw("Vertical");
            moveDir = new Vector3(dirX, 0, dirZ); // ���͂��ꂽ�l������Ƃ��Đݒ�
            moveDir.Normalize(); // ���K��
        }
    }

    // ���ۂ̈ړ����v�Z
    void CalculateMove()
    {
        if (gameObject != null)
        {
            moveValue = moveDir * currentSpeed;
            rb.velocity = new Vector3(moveValue.x, rb.velocity.y, moveValue.z); // �ړ��ʂ���
        }
    }

    //�@�J�����̂��Ⴊ�݋����𐧌䂷��
    void CrouchCamera()
    {
        float tagetY = isCrouch ? crouchCameraY : standCameraY; // ���Ⴊ�݃t���O��true��false�����f
        Vector3 currentPos = cameraTransform.localPosition; // ���݂̃J�����̈ʒu���擾
        float newY = Mathf.Lerp(currentPos.y, tagetY, Time.deltaTime * crouchSpeed); // targerY�܂ł̒l��⊮����(crouchSpeed�̒l�œ�����)
        cameraTransform.localPosition = new Vector3 (currentPos.x, newY, currentPos.z); // �J�����̈ʒu��ݒ�
    }


    //�@�W�����v�p�̓���
    void InputJump()
    {
        if (isJump) return; // ���łɃW�����v���Ȃ�I������
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isJump = true;
        }
    }

    //�@���n�p
    void OnCollisionEnter(Collision other)
    {
        if(!isJump) return; // �W�����v���łȂ��Ȃ�I��
        if (other.gameObject.CompareTag("Ground")) // �t�B�[���h�̃^�O��Ground�ɂ��Ă��邪�t�B�[���h�ǂ̗v�]�ɂ���ĕύX��
        {
            isJump = false;
        }
    }
}

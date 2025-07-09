using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//  �v���C���[�̈ړ���S������(FPS�Ȃ̂ŃJ�����ɂ���)
public class PlayerController : MonoBehaviour
{
    // ------------------------------------------�ϐ�---------------------------------

    [SerializeField] Transform cameraTransform; // ���Ⴊ�ލۂ̃J�����ړ��Ɏg��
    [SerializeField] Vector3 standCenter = new Vector3(0, 1.0f, 0); // �����Ă��鎞�̔���̒��S
    [SerializeField] Vector3 crouchCenter = new Vector3(0, 0.5f, 0); // ���Ⴊ��ł���Ƃ��̔���̒��S
    [SerializeField] int hp = 100;
    [SerializeField] float moveSpeed = 1.0f; // �ʏ�̈ړ����x
    [SerializeField] float standHeight = 1.5f; // �����Ă��鎞�̍���
    [SerializeField] float crouchMoveSpeed = 1.0f; //�@���Ⴊ�񂾎��̈ړ����x
    [SerializeField] float crouchHeight = 1.0f; // ���Ⴊ�񂾎��̍���
    [SerializeField] float crouchSpeed = 1.0f;�@//�@���Ⴊ�ނƂ��̃X�s�[�h
    [SerializeField] float dashMoveSpeed = 1.0f; // �_�b�V�����̈ړ����x
    [SerializeField] float jumpHeight = 1.0f; // �W�����v�̍���
    [SerializeField] float standCameraY = 1.5f; // �ʏ�̃J�����̍���
    [SerializeField] float crouchCameraY = 0.8f; // ���Ⴊ�񂾎��̃J�����̍���
    CapsuleCollider capsuleCollider; // ���Ⴊ�݂Ɏg��
    Rigidbody rb; // �ړ��Ɏg��
    Vector3 moveDir = Vector3.zero; // �ړ�����
    Vector3 moveValue = Vector3.zero; // �ړ������
    float currentSpeed = 0.0f; // ���݂̃X�s�[�h���擾
    float climingY = 0.0f; // ��q�����Ƃ��p�̕ϐ�
    bool isJump = false; // �W�����v�p�̃t���O
    bool isCrouch = false; // ���Ⴊ�ݗp�̃t���O
    bool isDash = false; // �_�b�V���p�̃t���O
    bool isCliming = false; // ��q�����Ƃ��p�̃t���O
    // ------------------------------------------�֐�---------------------------------------


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody���擾
        capsuleCollider = rb.GetComponent<CapsuleCollider>(); // �J�v�Z���R���C�_�[���擾
    }


    // ���͏�����Update
    void Update()
    {
        InputMove();
        InputJump();
    }

    //����������FixedUpdate�ŕ�����
    void FixedUpdate()
    {
        CalculateMove();
    }

    // Hp���擾����p
    public int GetHp()
    {
        return hp;
    }

    // Hp�ύX�p(���g��ς���)
    public void SetHp(int hp)
    {
        this.hp = hp;
    }


    //  �ړ��Ɋւ�����͂��󂯕t����
    void InputMove()
    {
        moveDir = Vector3.zero; // �ړ�����
        moveValue = Vector3.zero; // �ړ������

        if (this.gameObject != null)
        {


            // ��Ԑ؂�ւ��F�L�[���������u�u�ԁv�ł̂ݔ���
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                isCrouch = true;
                isDash = false;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (CanStandUp()) // ����ɏ�Q���Ȃ��Ƃ��̂݃_�b�V���Ɉڍs
                {
                    isDash = true;
                    isCrouch = false;
                }
            }

            // �����̃L�[�������ꂽ��A�ʏ��Ԃ�
            if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
            {
                if (CanStandUp())
                {
                    isCrouch = false;
                    isDash = false;
                }
            }

            // �X�s�[�h�ƃR���C�_�[�̐ݒ�
            if (isCrouch && !isCliming) // (��q�����Ƃ��͂��Ⴊ�߂Ȃ��悤�ɂ���)
            {
                currentSpeed = crouchMoveSpeed;
                capsuleCollider.height = crouchHeight;
                capsuleCollider.center = crouchCenter;
            }
            else if (isDash)
            {
                currentSpeed = dashMoveSpeed;
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }
            else
            {
                currentSpeed = moveSpeed;
                capsuleCollider.height = standHeight;
                capsuleCollider.center = standCenter;
            }


            CrouchCamera();

            // ��q�ɐڐG���Ă��Ȃ��Ȃ�
            if (!isCliming)
            {
                // �ړ�������ݒ�
                float dirX = Input.GetAxisRaw("Horizontal");
                float dirZ = Input.GetAxisRaw("Vertical");
                //�@�J�����̌����Ă�������ɐi�s���������킹��(���A���̓}�C�i�X�̒l�Ƃ��ĕ]������)
                Vector3 forward = cameraTransform.forward; // �J�����̑O�����̒l���擾
                Vector3 right = cameraTransform.right; // �J�����̉E�����̒l���擾
                                                       // y�������ɂ͊֗^���Ȃ�(����)
                forward.y = 0;
                right.y = 0;

                // �ʂ̕����𐳋K��
                forward.Normalize();
                right.Normalize();

                // �ړ��x�N�g���̌v�Z
                moveDir = (dirZ * forward + dirX * right).normalized;
            }
            // ��q�����Ƃ��p
            else
            {
                climingY = Input.GetAxis("Vertical"); // �㉺����
            }


        }
    }

    // ���ۂ̈ړ����v�Z
    void CalculateMove()
    {
        if (gameObject != null)
        {
            if (!isCliming)
            {
                moveValue = moveDir * currentSpeed;
                rb.velocity = new Vector3(moveValue.x, rb.velocity.y, moveValue.z); // �ړ��ʂ���
            }
            else
            {
                // rb.useGravity = false; // �������Ȃ��悤�ɂ���
                rb.velocity = new Vector3(0, climingY * currentSpeed, 0);
            }

        }
    }

    //�@�J�����̂��Ⴊ�݋����𐧌䂷��
    void CrouchCamera()
    {
        float tagetY = isCrouch ? crouchCameraY : standCameraY; // ���Ⴊ�݃t���O��true��false�����f
        Vector3 currentPos = cameraTransform.localPosition; // ���݂̃J�����̈ʒu���擾
        float newY = Mathf.Lerp(currentPos.y, tagetY, Time.deltaTime * crouchSpeed); // targerY�܂ł̒l��⊮����(crouchSpeed�̒l�œ�����)
        cameraTransform.localPosition = new Vector3(currentPos.x, newY, currentPos.z); // �J�����̈ʒu��ݒ�
    }


    //�@�W�����v�p�̓���
    void InputJump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && !isJump && !isCliming)
        {
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            isJump = true;
        }
    }

    //�@����ɂ��̂�����Η��ĂȂ�
    bool CanStandUp()
    {
        float headClearance = 0.1f; // �����]�T����������
        Vector3 rayOrigin = transform.position + capsuleCollider.center; // �L�������S
        float rayLength = (standHeight - crouchHeight) + headClearance; // �K�v�ȍ���

        // �f�o�b�O�p�� Ray �������i��:��Q������, ��:�N���A�j
        bool hit = Physics.Raycast(rayOrigin, Vector3.up, rayLength, ~0, QueryTriggerInteraction.Ignore);
        Color rayColor = hit ? Color.red : Color.green;
        Debug.DrawRay(rayOrigin, Vector3.up * rayLength, rayColor, 0.1f); // 0.1�b�\��

        return !hit; // �q�b�g���ĂȂ���Η��Ă�
    }

    //�@���n�p
    void OnCollisionEnter(Collision other)
    {
        if (!isJump) return; // �W�����v���łȂ��Ȃ�I��
        if (other.gameObject.CompareTag("Ground")) // �t�B�[���h�̃^�O��Ground�ɂ��Ă��邪�t�B�[���h�ǂ̗v�]�ɂ���ĕύX��
        {
            isJump = false;
        }
    }


    // ��q�p
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            float vertical = Input.GetAxis("Vertical");
            if (vertical != 0f && !isCliming)
            {
                isCliming = true;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                Debug.Log("��q�ɓ������i�o��/�~��j");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("LadderBottom"))
        {
            // S�L�[�i�������j�������Ă��Ȃ��Ȃ�AClimbing���f���Ȃ�
            float vertical = Input.GetAxis("Vertical");

            // �o���Ă��ԂŁA���͂���Ȃ� Climbing�p���A����ȊO�Ȃ璆�f
            if (isCliming && vertical <= 0f)
            {
                isCliming = false;
                rb.useGravity = true;
                Debug.Log("���[�ō~�肽��");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LadderTop"))
        {
            float vertical = Input.GetAxis("Vertical");
            if (isCliming && vertical > 0f)
            {
                isCliming = false;
                rb.useGravity = true;
                rb.velocity = cameraTransform.forward * moveSpeed;
                Debug.Log("��q�̏�ɓ��B�i�O�i�j");
            }
        }
    }

}

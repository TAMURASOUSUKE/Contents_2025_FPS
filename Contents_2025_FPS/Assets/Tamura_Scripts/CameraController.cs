using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //  ------------------------------------------------------�ϐ�------------------------------------------------
    [SerializeField] Transform playerBody; // �v���C���[(�e�I�u�W�F�N�g)
    [SerializeField] Transform cameraPivot; // �J�����̊�_
    [SerializeField] float topCameraLimit = 0.0f; // �J�����̏㑤�����̐���
    [SerializeField] float bottomCameraLimit = 0.0f; //�@�J�����̉����̌����̐���
    [SerializeField] float sensitivity = 2f; // ���x
    float xRotation = 0f; // �㉺��]�̒~��


    // --------------------------------------------------------�֐�-----------------------------------------
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �}�E�X�J�[�\������ʒ����ɌŒ�
        Cursor.visible = false; // �}�E�X�J�[�\�����\��
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; // x�������Ɋ��x��������
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity; // y�������Ɋ��x��������

        //�@�㉺�̉�]����
        xRotation -= mouseY;
        // ���_�̏��������ݒ�
        xRotation = Mathf.Clamp(xRotation, topCameraLimit, bottomCameraLimit); // X����]�͏�ɍs���قǕ��̒l���o��

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // ��]��K�p

        //�@�v���C���[�{�̂ō��E�̉�]�������s��
        playerBody.Rotate(Vector3.up * mouseX);
    }
}


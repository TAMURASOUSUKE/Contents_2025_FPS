using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //  ------------------------------------------------------�ϐ�------------------------------------------------
    [SerializeField] Transform playerBody; // �v���C���[(�e�I�u�W�F�N�g)
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
        xRotation = Mathf.Clamp(xRotation, -60.0f, 60.0f); // ���_�̏��������ݒ�

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); // ��]��K�p

        //�@�v���C���[�{�̂ō��E�̉�]�������s��
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

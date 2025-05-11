using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_input : MonoBehaviour
{
    public float sensitivity = 1.0f;
    public float rayDistance = 1f; // Ray�̋���
    public bool HaveArrow;
    public Transform marker; // ���W���������߂̃}�[�J�[�i��F�󒆂�Cube�Ȃǁj
    public Transform cameraTransform;
    public Transform character;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;  // sensitivity�͉�]���x
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        float input_WS = Input.GetAxis("Vertical");
        float input_AD = Input.GetAxis("Horizontal");
        
        Vector3 NowCamRotation = cameraTransform.rotation.eulerAngles;
        //Debug.Log("�J����" + NowCamRotation);

        if (NowCamRotation.x >= 50f && NowCamRotation.x < 100f)
        {
            if (-mouseY < 0)
            {
                cameraTransform.rotation = Quaternion.Euler(NowCamRotation.x + -mouseY, NowCamRotation.y, 0);
            }
            else
            {
                cameraTransform.rotation = Quaternion.Euler(NowCamRotation.x, NowCamRotation.y + mouseX, 0);
            }
        }
        else if (NowCamRotation.x <= 300f && NowCamRotation.x > 200f)
        {
            if (-mouseY > 0)
            {
                cameraTransform.rotation = Quaternion.Euler(NowCamRotation.x + -mouseY, NowCamRotation.y, 0);
            }
            else
            {
                cameraTransform.rotation = Quaternion.Euler(NowCamRotation.x, NowCamRotation.y + mouseX, 0);
            }
        }
        else
        {
            cameraTransform.rotation = Quaternion.Euler(NowCamRotation.x + -mouseY, NowCamRotation.y + mouseX, 0);
        }


        float angleInRadians = Mathf.Atan2(input_WS, input_AD);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        Vector3 targetRotation = character.rotation.eulerAngles;
        if (input_WS != 0 || input_AD != 0)
        {
            angleInDegrees -= 90;

            if (HaveArrow)
            {
                angleInDegrees -= 90;
            }
            angleInDegrees *= -1;
            //�J������rotation+Degrees�����Ȃ��Ⴂ���Ȃ�
            Vector3 cameraRotation = cameraTransform.rotation.eulerAngles;
            targetRotation.y = cameraRotation.y + angleInDegrees;
            character.rotation = Quaternion.Euler(targetRotation);
        }
        //Debug.Log("�L����" + targetRotation);
        //Debug.Log("�A���O��"+ angleInDegrees);


        // �J�����̒�������Ray���o��
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // �i�s������rayDistance�̋����ō��W���v�Z
        Vector3 pointInAir = ray.GetPoint(rayDistance);

        // �}�[�J�[�̈ʒu���󒆂̍��W�ɐݒ�
        if (marker != null)
        {
            marker.position = pointInAir;
        }







        
    }
}

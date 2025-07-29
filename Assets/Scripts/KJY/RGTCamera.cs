using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� �׽�Ʈ�� ī�޶�

public class RGTCamera : MonoBehaviour
{
    //ī�޶� ����ٴ� Ÿ��
    public GameObject Target;               

    public float offsetX = 0.0f;        
    public float offsetY = 10.0f;         
    public float offsetZ = -10.0f;        

    //ī�޶��� ���󰡴� �ӵ�
    public float CameraSpeed = 10.0f;
    //Ÿ���� ��ġ
    Vector3 TargetPos;                      


    void FixedUpdate()
    {
        //Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x + offsetX,
            Target.transform.position.y + offsetY,
            Target.transform.position.z + offsetZ
            );

        //ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
}


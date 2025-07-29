using UnityEngine;

public class JYADKey : MonoBehaviour
{

    public Rigidbody rollingRigidbody;
    //�¿� ȸ�� ��
    public float torqueForce = 10f; 
    //���� ��
    public float forwardForce = 15f; 
    //�ִ� ȸ�� �ӵ� ����
    public float maxAngularVelocity = 10f; 

    void Start()
    {
        //�ִ� ���ӵ� ���� (�ʹ� ������ ȸ������ �ʵ��� ����)
        rollingRigidbody.maxAngularVelocity = maxAngularVelocity;
    }

    void FixedUpdate()
    {
        //A Ű�� D Ű �Է� �ޱ�
        bool isAKeyPressed = Input.GetKey(KeyCode.A);
        bool isDKeyPressed = Input.GetKey(KeyCode.D);

        if (isAKeyPressed)
        {
            //A Ű�� ���� �� �������� ���� ��ũ ����
            rollingRigidbody.AddForce(Vector3.left * forwardForce, ForceMode.Force);
            rollingRigidbody.AddTorque(Vector3.up * -torqueForce, ForceMode.Force);
        }
        else if (isDKeyPressed)
        {
            //D Ű�� ���� �� ���������� ���� ��ũ ����
            rollingRigidbody.AddForce(Vector3.right * forwardForce, ForceMode.Force);
            rollingRigidbody.AddTorque(Vector3.up * torqueForce, ForceMode.Force);
        }
        else
        {
            //Ű �Է� ���� �� ���ӵ� ����
            rollingRigidbody.angularVelocity *= 0.95f;
        }
    }

}

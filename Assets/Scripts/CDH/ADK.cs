using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ADK : MonoBehaviour
{

    public Rigidbody rollingRigidbody; // ��ü�� Rigidbody
    public float torqueForce = 10f; // �¿� ȸ�� ��
    public float forwardForce = 15f; // ���� ��
    public float maxAngularVelocity = 10f; // �ִ� ȸ�� �ӵ� ����

    void Start()
    {
        // �ִ� ���ӵ� ���� (�ʹ� ������ ȸ������ �ʵ��� ����)
        rollingRigidbody.maxAngularVelocity = maxAngularVelocity;
    }

    void FixedUpdate()
    {
        // A Ű�� D Ű �Է� �ޱ�
        bool isAKeyPressed = Input.GetKey(KeyCode.A);
        bool isDKeyPressed = Input.GetKey(KeyCode.D);

        if (isAKeyPressed)
        {
            // A Ű�� ���� �� �������� ���� ��ũ ����
            rollingRigidbody.AddForce(Vector3.left * forwardForce, ForceMode.Force);
            rollingRigidbody.AddTorque(Vector3.up * -torqueForce, ForceMode.Force);
        }
        else if (isDKeyPressed)
        {
            // D Ű�� ���� �� ���������� ���� ��ũ ����
            rollingRigidbody.AddForce(Vector3.right * forwardForce, ForceMode.Force);
            rollingRigidbody.AddTorque(Vector3.up * torqueForce, ForceMode.Force);
        }
        else
        {
            // Ű �Է� ���� �� ���ӵ� ����
            rollingRigidbody.angularVelocity *= 0.95f;
        }
    }


}





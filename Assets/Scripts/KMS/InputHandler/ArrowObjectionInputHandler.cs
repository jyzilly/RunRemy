using UnityEngine;

public class ArrowObjectionInputHandler : MonoBehaviour, IInputHandler
{
    public InputType Type => InputType.ArrowObjection;
    public Vector3 HandleInput()
    {
        Debug.Log("Arrow Objection Input");

        float XAxis = Input.GetAxis("Horizontal");
        float YAxis = Input.GetAxis("Jump");

        // AD Ű�� ������ ��� ���� 0���� �����
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            XAxis = 0;
        }

        return new Vector3(-XAxis, YAxis, 0);
    }
}

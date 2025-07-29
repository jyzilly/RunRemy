using UnityEngine;

public class ADInputHandler : MonoBehaviour, IInputHandler
{
    public InputType Type => InputType.AD;
    public Vector3 HandleInput()
    {
        Debug.Log("ADŰ �Է� �޴���");

        float isAKeyPressed = Input.GetAxis("Horizontal");

        // ȭ��ǥ Ű�� ������ ��� ���� 0���� �����
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            isAKeyPressed = 0f;
        }

        return new Vector3(isAKeyPressed, 0, 0);
    }
}

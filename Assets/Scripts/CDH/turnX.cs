using UnityEngine;

public class turnX : MonoBehaviour
{
    public Transform tr;
    // ȸ�� �ӵ��� �����մϴ� (����: ��/��).
    public float rotationSpeed = 100f;

    void LateUpdate()
    {
        // Y���� �������� ȸ���մϴ�.
        tr.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}

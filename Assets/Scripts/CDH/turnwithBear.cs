using UnityEngine;

public class turnwithBear : MonoBehaviour
{
    public Transform tr;
    // ȸ�� �ӵ��� �����մϴ� (����: ��/��).
    public float rotationSpeed = 100f;

    void LateUpdate()
    {
        // Y���� �������� ȸ���մϴ�.
        tr.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}

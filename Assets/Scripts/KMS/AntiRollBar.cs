using UnityEngine;

/// <summary>
/// ���� ���� ���� ���� ������ �ð��� ������(�Ѹ�)�� �����ϴ� ��ũ��Ʈ
/// </summary>
public class AntiRollBar : MonoBehaviour
{
    [SerializeField] private Transform body;  // ��ü(Body) �� Transform
    [SerializeField] private WheelCollider leftWheel;
    [SerializeField] private WheelCollider rightWheel;

    [SerializeField] private float maxTiltAngle = 10f;  // �ִ� ���� (Roll)
    [SerializeField] private float tiltMultiplier = 5f; // ���� �ΰ���

    private void Update()
    {
        float travelL = GetWheelTravel(leftWheel);
        float travelR = GetWheelTravel(rightWheel);

        // �¿� ���� ������� ���̸� ������� �Ѹ� ���
        float tiltAngle = (travelL - travelR) * tiltMultiplier;
        tiltAngle = Mathf.Clamp(tiltAngle, -maxTiltAngle, maxTiltAngle);

        // ��ü(Body) ����̱� (Vector3.right �������� �Ѹ� ����)
        body.localRotation = Quaternion.Euler(0, 0, tiltAngle);
    }

    // ������� ���� ����(������ ���� �̵���) ���
    private float GetWheelTravel(WheelCollider wheel)
    {
        WheelHit hit;
        if (wheel.GetGroundHit(out hit))
        {
            return (-wheel.transform.InverseTransformPoint(hit.point).y - wheel.radius) / wheel.suspensionDistance;
        }
        return 0f; // ���� ���� ���� ��� ������ ����
    }
}

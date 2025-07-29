using UnityEngine;

public class CarStabilizer : MonoBehaviour
{
    private Rigidbody rb;
    // �ִ� ��� ��(�¿� ����) ���� (��: 10��)
    public float maxRollAngle = 10f;
    // ȸ�� ���� ���� �ӵ� (0~1 ������ ��, 1�̸� ��� ����)
    public float rotationLerpFactor = 0.1f;

    // ���� ����: ��ġ(�յ� ����)�� �ּ�/�ִ� ���� ����
    // ��� ���� �� �ʿ信 ���� ��� ������ �а� ���� �� �ֽ��ϴ�.
    public float minPitchAngle = -60f;
    public float maxPitchAngle = 60f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ���� ȸ���� Euler ������ ��ȯ (Unity�� Euler�� 0~360�̹Ƿ� -180~180�� ��ȯ�Ͽ� �ٷ�)
        Vector3 currentEuler = rb.rotation.eulerAngles;
        float currentPitch = NormalizeAngle(currentEuler.x);
        float currentYaw = currentEuler.y;   // ȸ�� ����� ���� ������ �ʿ���ٸ� �״�� ���
        float currentRoll = NormalizeAngle(currentEuler.z);

        // ���� �ݵ�� ���� (�¿� ��ħ ����)
        float clampedRoll = Mathf.Clamp(currentRoll, -maxRollAngle, maxRollAngle);

        // ��ġ�� ��Ȳ�� ���� ��� ���� ��� (���⼭�� ���� �����Դϴ�)
        float clampedPitch = Mathf.Clamp(currentPitch, minPitchAngle, maxPitchAngle);

        // Ÿ�� ȸ�� ����: ��ġ�� ���� Ŭ������ ��, ��� �״�� ����
        Quaternion targetRotation = Quaternion.Euler(clampedPitch, currentYaw, clampedRoll);

        // �ε巯�� ���� ���� (Slerp ���)
        Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationLerpFactor);
        rb.MoveRotation(newRotation);
    }

    // Euler ������ -180 ~ 180 ������ ����ȭ
    float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }
}

using UnityEngine;

public class AntiRollPlus : MonoBehaviour
{
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheel;
    [SerializeField] private WheelCollider frontRightWheel;
    [SerializeField] private WheelCollider rearLeftWheel;
    [SerializeField] private WheelCollider rearRightWheel;

    [Header("���� ����")]
    [SerializeField] private float alignmentSpeed = 10f;
    [SerializeField] private bool showDebugRays = true;  // ����� ���� ǥ�� ����

    private void Update()
    {
        // �� ���� ��ġ������ ���� ��� ���
        Vector3 flNormal = GetWheelSurfaceNormal(frontLeftWheel);
        Vector3 frNormal = GetWheelSurfaceNormal(frontRightWheel);
        Vector3 rlNormal = GetWheelSurfaceNormal(rearLeftWheel);
        Vector3 rrNormal = GetWheelSurfaceNormal(rearRightWheel);

        // ��� ������ ���鿡 ����ִ��� Ȯ��
        if (flNormal != Vector3.zero && frNormal != Vector3.zero &&
            rlNormal != Vector3.zero && rrNormal != Vector3.zero)
        {
            // ��� ��� ���
            Vector3 averageNormal = (flNormal + frNormal + rlNormal + rrNormal).normalized;

            // Ÿ�� ȸ�� ���
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, averageNormal) * transform.rotation;

            // �ε巴�� ȸ�� ����
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * alignmentSpeed);
        }
    }

    private Vector3 GetWheelSurfaceNormal(WheelCollider wheel)
    {
        WheelHit hit;
        if (wheel.GetGroundHit(out hit))
        {
            if (showDebugRays)
            {
                Debug.DrawRay(hit.point, hit.normal * 1f, Color.green);
            }
            return hit.normal;
        }
        return Vector3.zero;
    }

}

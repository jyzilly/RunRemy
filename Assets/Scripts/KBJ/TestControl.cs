using UnityEngine;

public class RollingController : MonoBehaviour
{
    public Rigidbody rb;
    public float moveForce = 10f;  // ���ӷ�
    public float turnSpeed = 2f;   // ȸ�� �ӵ�
    public float brakeForce = 8f;  // ���ӷ�

    private Vector3 forwardDirection; // ���� ���� ����

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        forwardDirection = transform.forward; // �ʱ� ���� ����
    }

    void FixedUpdate()
    {
        // ����� ���� (���� ���� ���� �ð�ȭ)
        Debug.DrawLine(transform.position, transform.position + forwardDirection * 5f, Color.green);

        // WŰ: ���� forwardDirection �������� ����
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(forwardDirection * moveForce, ForceMode.Acceleration);
        }

        // SŰ: ���� ������ �ݴ� �������� ����
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-forwardDirection * brakeForce, ForceMode.Acceleration);
        }

        // A/DŰ: ȸ�� (forwardDirection�� ����)
        if (Input.GetKey(KeyCode.A))
        {
            forwardDirection = Quaternion.Euler(0, -turnSpeed, 0) * forwardDirection;
        }
        if (Input.GetKey(KeyCode.D))
        {
            forwardDirection = Quaternion.Euler(0, turnSpeed, 0) * forwardDirection;
        }
    }
}

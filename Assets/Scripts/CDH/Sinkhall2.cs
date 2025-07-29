using UnityEngine;

public class Sinkhall2 : MonoBehaviour
{
    public Rigidbody rb;  // �������� ������ٵ�
    public float sinkSpeed = 1f;  // ����ɴ� �ӵ�
    public float maxSinkDepth = 3f;  // �ִ� ������� ����
    public float riseSpeed = 2f;  // �ö���� �ӵ�
    public int keyPressThreshold = 5;  // ����/������ Ű ��Ÿ Ƚ��
    private int keyPressCount = 0;  // ����Ű ��Ÿ ī��Ʈ
    private bool isSinking = false;  // ���� ����ɰ� �ִ��� ����
    private bool isRising = false;  // ���� �ö󰡰� �ִ��� ����
    private Vector3 originalPosition;  // ���� ��ġ

    private void Start()
    {
        originalPosition = transform.position;  // ���� ��ġ ����
    }

    private void Update()
    {
        // ������ �ӵ� üũ (60 ������ �� ����ɱ� ����)
        if (rb.linearVelocity.magnitude < 60f && !isRising)
        {
            isSinking = true;
        }

        if (isSinking)
        {
            // ����ɱ� (Rigidbody�� ����Ͽ� �ڿ������� ����ɵ��� ����)
            float targetY = originalPosition.y - maxSinkDepth;
            float step = sinkSpeed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

            // Rigidbody�� ��ġ ����
            rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, step));

            // �ִ� ����ɱ� ���̿� �����ϸ� ��Ÿ ����
            if (transform.position.y <= targetY)
            {
                isSinking = false;  // ����ɱⰡ ������ ��Ÿ ���
            }
        }

        // ����Ű ��Ÿ ����
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            keyPressCount++;
            if (keyPressCount >= keyPressThreshold && !isRising)
            {
                isRising = true;
            }
        }

        // �ö����
        if (isRising)
        {
            float targetY = originalPosition.y;
            float step = riseSpeed * Time.deltaTime;
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

            // Rigidbody�� ��ġ ����
            rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, step));

            if (transform.position.y >= targetY)
            {
                isRising = false;
                // ������ �ö���� ��¦ ƨ��� (���� ƨ��� ȿ��)
                rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
            }
        }
    }

    // ����ɱ� �� �ӵ��� ���� ���� �������� ���� �� �ִ� �ڵ� (�߷� ����)
    private void FixedUpdate()
    {
        if (isSinking)
        {
            // �߷��� �߰��� �����Ͽ� �� ������ ���������� �� �� �ֽ��ϴ�.
            rb.AddForce(Vector3.down * sinkSpeed, ForceMode.Acceleration);
        }
    }
}

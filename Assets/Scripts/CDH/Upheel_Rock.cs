using UnityEngine;

public class Upheel_Rock : MonoBehaviour
{
    public float destroyHeight = -10f; // �� ���̺��� �������� �ڵ� ����
    public GameObject explosionEffect; // ���� ȿ�� ������


    public float minGravity = 30f;  // �ּ� �߷�
    public float maxGravity = 60f; // �ִ� �߷�
    public float minPushForce = 5f;  // �ּ� �߰� �ӵ�
    public float maxPushForce = 50f; // �ִ� �߰� �ӵ�

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //rb.useGravity = false;
        // ������ �ٸ� �߷� ȿ�� ����
        float randomGravity = Random.Range(minGravity, maxGravity);
        rb.AddForce(Vector3.down * randomGravity * 1000f, ForceMode.Acceleration);

        // ������ ���� �߰��ؼ� ������ �ӵ� ���̸� ��
        float randomPush = Random.Range(minPushForce, maxPushForce);
        rb.AddForce(new Vector3(Random.Range(-1f, 1f), -2, 0) * randomPush, ForceMode.Impulse); // �¿�� ��¦ ��鸮�鼭 �Ʒ��� ������
    }

    private void Update()
    {
        // ���� �ʹ� �Ʒ��� �������� ����
        if (transform.position.y <= destroyHeight)
        {
            DestroyRock();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� �ε����� ���� �� ���� ȿ�� ����
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DestroyRock()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity); // ���� ȿ�� ����
        }
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Word : MonoBehaviour
{
    public float fallSpeed = 2f; // �ܾ �������� �ӵ�

    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime); // �ܾ �Ʒ��� ������

        // ȭ�� ������ ������ �ܾ� ����
        if (transform.position.y < 100f)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPower = 100f;
    public float deceleration = 10f;

    private void OnTriggerEnter(Collider collider)
    {
        // ���̾�� ���͸�
        if (collider.gameObject.layer == LayerMask.NameToLayer("SphereRB"))
        {
            Debug.Log("���� ����");
            // �浹�� ������Ʈ�� Rigidbody ������Ʈ�� ������
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            // Rigidbody ������Ʈ�� ���ٸ� ����
            if (rb == null)
            {
                return;
            }
            // Rigidbody ������Ʈ�� ���� ����
            rb.transform.position += new Vector3(0, 1f, 0);
            rb.linearVelocity /= deceleration;
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            AudioManager.instance.PlaySfx(AudioManager.sfx.funscream);
        }

    }
}

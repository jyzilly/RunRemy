using UnityEngine;

public class AccelPlatform : MonoBehaviour
{
    private float speed = 4.5f;

    private void OnTriggerEnter(Collider collider)
    {
        // ���̾�� ���͸�
        if (collider.gameObject.layer == LayerMask.NameToLayer("SphereRB"))
        {
            // �浹�� ������Ʈ�� Rigidbody ������Ʈ�� ������
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            // Rigidbody ������Ʈ�� ���ٸ� ����
            if (rb == null)
            {
                return;
            }
            // Rigidbody ������Ʈ�� ���� ����
            //rb.AddForce(transform.forward * 100f, ForceMode.Impulse);
            rb.linearVelocity *= speed;
            //rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, 100);
            AudioManager.instance.PlaySfx(AudioManager.sfx.manscream);
        }

    }
}

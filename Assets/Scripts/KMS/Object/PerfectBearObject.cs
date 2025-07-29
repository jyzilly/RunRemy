using UnityEngine;

public class PerfectBearObject : InteractableObject
{

    private void Start()
    {
        // �θ� �ݶ��̴��� �ֻ�� ��ġ ���
        float topY = col.bounds.max.y;                
        //float spawnOffset = 2f; // ��¦ ���� ���� �Ÿ�

        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            topY,
            transform.position.z
        );

        Resources.Load<GameObject>("HeartParticle");

        // Star ���� �� �θ� ����
        GameObject heart = Instantiate(star, spawnPosition, Quaternion.identity, transform);

        heart.transform.localPosition = transform.InverseTransformPoint(spawnPosition);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        AudioManager.instance.PlaySfx(AudioManager.sfx.bear);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        AudioManager.instance.PlaySfx(AudioManager.sfx.bear);
    }


}

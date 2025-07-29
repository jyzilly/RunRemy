using UnityEngine;

public class HandFollowBird : MonoBehaviour
{
    public Rigidbody handRb; // ���� Rigidbody
    public Rigidbody birdRb; // ���� Rigidbody
    public Animator birdAnimator; // ���� �ִϸ�����
    public ConfigurableJoint joint; // �հ� ���� �����ϴ� ����Ʈ

    void Start()
    {
       

        // ���� Rigidbody�� Joint�� ����
        joint.connectedBody = birdRb;

        // Joint ���� (���� �þ�� �ʵ��� ����)
        joint.xMotion = ConfigurableJointMotion.Locked;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.linearLimit = new SoftJointLimit { limit = 0.01f }; // ���� �Ÿ� ����

        // ��Ŀ ��ġ ���� (�հ� �� �ٸ��� ��Ȯ�� ����ǵ���)
        joint.anchor = Vector3.zero;
        joint.connectedAnchor = new Vector3(0, -0.1f, 0); // �� �ٸ� �Ʒ��ʿ� ����
    }

    void FixedUpdate()
    {
        // �ִϸ��̼��� Transform.position�� ���� �����ϴ� ���, Rigidbody�� ����ȭ
        birdRb.MovePosition(birdAnimator.transform.position);
    }
}

using UnityEngine;

public class mocma : MonoBehaviour
{
    //[SerializeField] private float moveSpeed = 10f;  // ������ ���� �ӵ�
    [SerializeField] private float turnSpeed = 50f;  // ȸ�� �ӵ�
    //[SerializeField] private float tiltAmount = 30f;  // �������� ����
    //[SerializeField] private float smoothTime = 0.3f;  // �������� �ӵ�
    [SerializeField] private float sideMoveFactor = 5f;  // �¿� �̵� ��
    [SerializeField] private Rigidbody[] ragdollLimbs;  // ���׵� ��(����)��
    [SerializeField] private float ragdollFlapForce = 10f;  // ���׵��� �ȶ��Ÿ��� ��

    public Rigidbody rb;
    private float targetTilt;
    private float currentVelocity;

    //private void Start()
    //{
    //    //rb = GetComponent<Rigidbody>();
    //    rb.useGravity = true;  // �߷� ����
    //}

    private void FixedUpdate()
    {
        //rb.linearVelocity = transform.forward * moveSpeed;

            FlapRagdoll(Vector3.back);  // ���׵� ���� (����)

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(Vector3.up * turnSpeed);
            //targetTilt = tiltAmount;
            rb.linearVelocity += Vector3.right * sideMoveFactor * Time.deltaTime;
            FlapRagdoll(Vector3.left);  // ���׵� ���� (����)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(Vector3.up * -turnSpeed);
           // targetTilt = -tiltAmount;
            rb.linearVelocity += Vector3.left * sideMoveFactor * Time.deltaTime;
            FlapRagdoll(Vector3.right);  // ���׵� ���� (������)
        }
        //else
        //{
        //    targetTilt = 0;
        //}

        //float newZRotation = Mathf.SmoothDampAngle(transform.eulerAngles.z, targetTilt, ref currentVelocity, smoothTime);
       // transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, newZRotation);
    }

    private void FlapRagdoll(Vector3 direction)
    {
        foreach (Rigidbody limb in ragdollLimbs)
        {
            Vector3 randomForce = direction * -1 * ragdollFlapForce * Random.Range(0.8f, 1.2f);
            limb.AddForce(randomForce, ForceMode.Impulse);  // ������ ���� �߰�
        }
    }
}







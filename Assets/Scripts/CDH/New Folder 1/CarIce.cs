using UnityEngine;
using UnityEngine.Splines;

public class CarIce : MonoBehaviour
{
    public Rigidbody carrb;
    //public GameObject playerObject;
    private Rigidbody Ragrb;
    public float slideForce = 5f; // �⺻ �̲����� ��
    public float minSlideAngle = 10f; // �ּ� �̲����� ��� ����
    public float maxSlideMultiplier = 2f; // �ִ� �̲������� �� ������


    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Ragrb = playerObject.GetComponent<Rigidbody>();
        }
    }


    //private void Update()
    //{
    //    Debug.DrawLine(transform.position, Vector3.down * 2f, Color.red);
    //    Debug.Log("�� ��ũ��Ʈ ���̰� ����");
    //}

    //    void FixedUpdate()
    //    {
    //        Debug.Log("���Ǳ� �ö��");

    //        Debug.DrawLine(transform.position, Vector3.down * 2f, Color.red);

    //        Debug.Log("���ӷ�" + carrb.linearVelocity.magnitude);
    //        Debug.Log("����ӷ�" + Ragrb.linearVelocity.magnitude);

    //        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
    //        {

    //            Debug.Log("���Ǳ� �ö��2");
    //            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    //            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;

    //            Debug.DrawRay(hit.point, hit.normal * 1f, Color.green);



    //            // ��簡 ���� �̻��̰�, �ӵ��� ������ �̲�����
    //            if (slopeAngle > minSlideAngle && carrb.linearVelocity.magnitude < 2f)
    //            {
    //                float slideMultiplier = Mathf.Clamp(slopeAngle / 45f, 1f, maxSlideMultiplier); // ��簢�� ���� �� ����
    //                carrb.AddForce(slopeDirection * (slideForce * slideMultiplier), ForceMode.Force);
    //            }
    //        }
    //    }

    //}


    ////using UnityEngine;

    ////public class CarIce : MonoBehaviour
    ////{
    ////    public float slideForce = 10f; // �̲������� �⺻ ��
    ////    public float minSlideAngle = 15f; // �̲������� ������ �ּ� ��簢
    ////    public float maxSlideMultiplier = 2f; // �ִ� �̲����� ����
    ////    public float speedLimit = 3f; // �ӵ� ����

    ////    public Rigidbody rb; // �������� Rigidbody ������Ʈ

    ////    void Start()
    ////    {
    ////        // Rigidbody ������Ʈ ��������
    ////        //rb = GetComponent<Rigidbody>();

    ////        // Rigidbody ���� ���� (�ε巯�� ���� ���)
    ////        rb.interpolation = RigidbodyInterpolation.Interpolate;
    ////    }

    ////    void FixedUpdate()
    ////    {
    ////        // ������ �����ϱ� ���� Raycast
    ////        Debug.DrawLine(transform.position, Vector3.down * 2f, Color.red);

    ////        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
    ////        {
    ////            // ��簢 ���
    ////            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    ////            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;

    ////            // ������ ���� �̲������� ���� �ð�ȭ
    ////            Debug.DrawRay(hit.point, hit.normal * 1f, Color.green);

    ////            // ��簢�� ���� �̻��̰�, �ӵ��� ������ �̲�����
    ////            if (slopeAngle > minSlideAngle && rb.linearVelocity.magnitude < speedLimit)
    ////            {
    ////                // �̲����� ���� ��� (��簢�� ���)
    ////                float slideMultiplier = Mathf.Clamp(slopeAngle / 45f, 1f, maxSlideMultiplier);

    ////                // �̲����� �� ���
    ////                float slideForceToApply = slideForce * slideMultiplier;

    ////                // �̲����� ���� �ε巴�� ����
    ////                rb.AddForce(slopeDirection * slideForceToApply * Time.deltaTime, ForceMode.Force);
    ////            }
    ////        }

    ////        // �������� �ӵ� ���� (�ʹ� ������ �̲������� �ʵ���)
    ////        if (rb.linearVelocity.magnitude > speedLimit)
    ////        {
    ////            rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
    ////        }
    ////    }
    ////}


    ////using UnityEngine;

    ////public class CarIce : MonoBehaviour
    ////{
    ////    public float slideForce = 10f; // �̲������� �⺻ ��
    ////    public float minSlideAngle = 15f; // �̲������� ������ �ּ� ��簢
    ////    public float maxSlideMultiplier = 2f; // �ִ� �̲����� ����
    ////    public float speedLimit = 3f; // �ӵ� ����

    ////    public Rigidbody rb; // �������� Rigidbody ������Ʈ

    ////    void Start()
    ////    {
    ////        // Rigidbody ������Ʈ ��������
    ////        //rb = GetComponent<Rigidbody>();

    ////        // Rigidbody ���� ���� (�ε巯�� ���� ���)
    ////        rb.interpolation = RigidbodyInterpolation.Interpolate;
    ////    }

    ////    void FixedUpdate()
    ////    {
    ////        // ������ �����ϱ� ���� Raycast
    ////        Debug.DrawLine(transform.position, Vector3.down * 2f, Color.red);

    ////        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
    ////        {
    ////            // ��簢 ���
    ////            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
    ////            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;

    ////            // ������ ���� �̲������� ���� �ð�ȭ
    ////            Debug.DrawRay(hit.point, hit.normal * 1f, Color.green);

    ////            // ��簢�� ���� �̻��̰�, �ӵ��� ������ �̲�����
    ////            if (slopeAngle > minSlideAngle && rb.linearVelocity.magnitude < speedLimit)
    ////            {
    ////                // �̲����� ���� ��� (��簢�� ���)
    ////                float slideMultiplier = Mathf.Clamp(slopeAngle / 45f, 1f, maxSlideMultiplier);

    ////                // �̲����� �� ���
    ////                float slideForceToApply = slideForce * slideMultiplier;

    ////                // �����Ű� ȸ���� ������ ���鿡 �°� �̲������� ����
    ////                Vector3 slideDirection = Vector3.ProjectOnPlane(transform.forward, hit.normal).normalized;

    ////                // �̲����� ���� �ε巴�� ����
    ////                rb.AddForce(slideDirection * slideForceToApply * Time.deltaTime, ForceMode.Force);
    ////            }
    ////        }

    ////        //�������� �ӵ� ����(�ʹ� ������ �̲������� �ʵ���)
    ////        if (rb.linearVelocity.magnitude > speedLimit)
    ////        {
    ////            rb.linearVelocity = rb.linearVelocity.normalized * speedLimit;
    ////        }
    ////    }
    ////}




    void FixedUpdate()
    {
        Debug.Log("���Ǳ� �ö��");

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 2f, Color.red);
        Debug.Log("���ӷ�: " + carrb.linearVelocity.magnitude);
        Debug.Log("����ӷ�: " + Ragrb.linearVelocity.magnitude);


        //Debug.Log("Ray Start Position: " + (transform.position + Vector3.up * 1.3f));
        //if (Physics.Raycast(transform.position + Vector3.up * 1.3f, Vector3.down, out RaycastHit hit, 5f))
        //{
        //    Debug.Log("Ray Hit Point: " + hit.point);
        //    Debug.DrawRay(hit.point, slopeDirection * 2f, Color.blue);
        //}


        Debug.Log("Ray Start Position: " + (transform.position + Vector3.up * 1.3f));
        if (Physics.Raycast(transform.position + Vector3.up * 1.3f, Vector3.down, out RaycastHit hit))
        {
            Debug.Log("���Ǳ� �ö��2");

            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.down, hit.normal).normalized;
            //Vector3 slopeDirection = Vector3.ProjectOnPlane(Vector3.forward, hit.normal).normalized;


            Debug.Log("Ray Hit Point: " + hit.point);

            Debug.DrawRay(hit.point, hit.normal * 10f, Color.green); // ������ ���� ���͸� �ð������� Ȯ��

            Debug.DrawRay(hit.point, slopeDirection * 10f, Color.blue);

            Debug.Log("���ǰ���" + slopeAngle);
            Debug.DrawRay(hit.point, hit.normal * 1f, Color.yellow);

            //  **�������� (�̲�������)**
            if (slopeAngle > minSlideAngle)
            {
                float slideMultiplier = Mathf.Clamp(slopeAngle / 30f, 1f, maxSlideMultiplier); // ��簢�� ���� �� ����
                carrb.AddForce(slopeDirection * (slideForce * slideMultiplier), ForceMode.Acceleration);

                // �������濡�� ���ӵǵ��� �ӵ��� ������Ŵ
                carrb.linearVelocity += slopeDirection * Time.fixedDeltaTime * 5f; // �ڿ������� �� ������


                // �ִ� �ӵ� ����
                float maxSlideSpeed = 20f;
                if (carrb.linearVelocity.magnitude > maxSlideSpeed)
                {
                    carrb.linearVelocity = carrb.linearVelocity.normalized * maxSlideSpeed;
                }
            }

            //  **�������� (�ӵ� ���� �����)**
            if (slopeAngle > minSlideAngle && Vector3.Dot(carrb.linearVelocity.normalized, slopeDirection) < 0)
            {
                carrb.linearVelocity *= 0.98f; // �������濡���� �ӵ��� �ڿ������� �پ��
            }
        }
        else
        {
            Debug.Log("Ray ����Ʈ! ���� ã�� ����!");
        }
    }
}

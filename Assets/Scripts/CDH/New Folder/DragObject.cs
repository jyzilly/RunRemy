using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 10f;  // ���콺 �̵� �ӵ�
    public float smoothTime = 0.2f; // �ε巯�� �̵�

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButton(0)) // ���콺 ���� Ŭ�� ��
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero); // ��� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 targetPos = ray.GetPoint(distance);
                Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

                rb.MovePosition(smoothedPos); // ���� �̿��� �̵�
            }
        }
    }
}

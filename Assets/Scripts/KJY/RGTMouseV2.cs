using UnityEngine;

//��ü �ڿ������� ������ �̵��ϰ� �ְ�, ���콺 Ŭ������ �� ��ü�� X��ǥ�� ���콺�� X��ǥ�� ���󰣴�.
//���� �̿��Ͽ� �̵��ؾ� �Ѵ�.

public class RGTMouseV2 : MonoBehaviour
{


    [SerializeField] private GameObject hat;
    [SerializeField] private Rigidbody[] ragdollLimbs;

    //���� ������Ʈ�� ���콺�� ���� �巡�׵ǰ� �ִ��� ���ο�
    private bool isDragging = false;
    //�¿� ȸ�� ��
    private float torqueForce = 10f; 
    //���� ��
    public float forwardForce = 15f; 


    private void Update()
    {
        FallowThePos(hat);
    }

    //���콺 �巡���� ��
    private void GetMouseButton()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.tag == "hat")
                {
                    isDragging = true;
                }

            }
        }
        else
        {
            isDragging = false;
        }

    }

    //hat �����ϴ��Լ�
    public void FallowThePos(GameObject _object)
    {
        Rigidbody rb = _object.GetComponent<Rigidbody>();
        GameObject Object = _object;

        //���������� �����ϴ� ���� ���Ѵ�.
        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
        //���������� Y���� �������� ȸ���ϴ� Torqur�� ���Ѵ�.
        rb.AddTorque(Vector3.up * -torqueForce, ForceMode.Force);

        //���콺�� �Է� ���¸� Ȯ��
        GetMouseButton();

        if(isDragging)
        {
            //������ x��ǥ, ���콺�� x��ǥ ���� ����.

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Vector3 newPosition = Object.transform.position;

                //���콺�� ����Ű�� ������ X��ǥ�� ������Ʈ�� X��ǥ�� ��� �ݿ�
                newPosition.x = hit.point.x;
                Object.transform.position = newPosition;
            }
        }
    }


}

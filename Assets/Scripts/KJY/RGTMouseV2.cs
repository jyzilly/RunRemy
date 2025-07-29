using UnityEngine;

//물체 자연스럽게 앞으로 이동하고 있고, 마우스 클릭했을 때 물체의 X좌표는 마우스의 X좌표를 따라간다.
//힘을 이용하여 이동해야 한다.

public class RGTMouseV2 : MonoBehaviour
{


    [SerializeField] private GameObject hat;
    [SerializeField] private Rigidbody[] ragdollLimbs;

    //현재 오브젝트가 마우스에 의해 드래그되고 있는지 여부용
    private bool isDragging = false;
    //좌우 회전 힘
    private float torqueForce = 10f; 
    //전진 힘
    public float forwardForce = 15f; 


    private void Update()
    {
        FallowThePos(hat);
    }

    //마우스 드래그할 때
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

    //hat 제어하는함수
    public void FallowThePos(GameObject _object)
    {
        Rigidbody rb = _object.GetComponent<Rigidbody>();
        GameObject Object = _object;

        //지속적으로 전진하는 힘을 가한다.
        rb.AddForce(Vector3.forward * forwardForce, ForceMode.Force);
        //지속적으로 Y축을 기준으로 회전하는 Torqur를 가한다.
        rb.AddTorque(Vector3.up * -torqueForce, ForceMode.Force);

        //마우스의 입력 상태를 확인
        GetMouseButton();

        if(isDragging)
        {
            //모자의 x좌표, 마우스의 x좌표 따라 간다.

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Vector3 newPosition = Object.transform.position;

                //마우스가 가리키는 지점의 X좌표를 오브젝트의 X좌표에 즉시 반영
                newPosition.x = hit.point.x;
                Object.transform.position = newPosition;
            }
        }
    }


}

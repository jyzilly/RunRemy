using UnityEngine;


public class Hat : MonoBehaviour
{
    //[SerializeField] private AnimationClip Anim;
    //[SerializeField] private Animator animator;
    [SerializeField] Transform TheBall;
    [SerializeField] Rigidbody CharacterRigidbody;

    // �� ������ �߽��� ��� ����
    public float balanceHeight = 0f;

    //[SerializeField] private GameObject hpBarPrefab = null;
    //[SerializeField] private RGTHpBar hpBar = null;

    private void Start()
    {
        CharacterRigidbody.isKinematic = true;
        //Animator animator = GetComponent<Animator>();



    }

    private void FixedUpdate()
    {
        // ĳ���Ͱ� �� ���� ������ ���̿� �ֵ��� ����
        transform.position = TheBall.position + Vector3.up * balanceHeight;
    }


    private void Update()
    {
        //hpBar.UpdatePosition(transform.position);

        //animator.SetBool("SetLeftActive", true);

        RotateZ(Thedir());
        RotateX(Thedir());
        //ȸ������ �����ϴ� �ɷ� �ٲ�� ��
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            //transform.eulerAngles += Vector3.forward * balanceSpeed * Time.deltaTime;
            //transform.Rotate(0f,0f,+RotationValue);
            //animator.SetBool("SetLeftActive", false);
            RotateZ(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //animator.SetBool("SetRightActive", false);
            //transform.eulerAngles += Vector3.back * balanceSpeed * Time.deltaTime;
            RotateZ(-1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //transform.eulerAngles += Vector3.right * balanceSpeed * Time.deltaTime;
            //animator.SetBool("SetUpActive", false);
            RotateX(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //transform.eulerAngles += Vector3.up * balanceSpeed * Time.deltaTime;
            //animator.SetBool("SetBackActive", false);
            RotateX(-1);
        }

        //Debug.Log("z : " + TheObject.transform.rotation.eulerAngles.z);
        CheckRotation();


    }

    private float Thedir()
    {

        Vector3 center = transform.position;
        Vector3 headPoint = transform.up + transform.position;

        Vector3 dir = headPoint - center;
        float a = 0;
        float angle = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;

        if (angle > 0)
        {
            a = 0.3f;
        }
        else
        {
            a = -0.3f;
        }
        //Debug.DrawLine(center,headPoint,Color.green);
        //Debug.Log("������ " + angle + "a :" + a);
        return a;
    }

    private void RotateX(float _direction)
    {
        //float xRotation = _direction * balanceSpeed * Time.deltaTime;
        ////transform.Rotate(xRotation, 0f, 0f, Space.Self);

        //float newXRotation = transform.eulerAngles.x + xRotation;

        //// ����Ƽ�� EulerAngles�� 0~360���� ǥ���ǹǷ� -60~60���� ��ȯ�ؾ� ��
        //if (newXRotation > 180f)
        //    newXRotation -= 360f;

        //newXRotation = Mathf.Clamp(newXRotation, -60f, 60f);

        //transform.rotation = Quaternion.Euler(newXRotation, 0f, 0f);
        transform.Rotate(Vector3.right, _direction * Time.deltaTime * 50f);
    }

    private void RotateZ(float _direction)
    {
        //float zRotation = _direction * balanceSpeed * Time.deltaTime;
        ////transform.Rotate( 0f, 0f, zRotation, Space.Self);
        //float newZRotation = transform.eulerAngles.z + zRotation;

        //// ����Ƽ�� EulerAngles�� 0~360���� ǥ���ǹǷ� -60~60���� ��ȯ�ؾ� ��
        //if (newZRotation > 180f)
        //    newZRotation -= 360f;

        //newZRotation = Mathf.Clamp(newZRotation, -60f, 60f);

        //transform.rotation = Quaternion.Euler(0f, 0f, newZRotation);
        transform.Rotate(Vector3.forward, _direction * Time.deltaTime * 50f);
    }






    //üũ ȸ�� ��
    private void CheckRotation()
    {
        //���� üũ�ϱ� ���࿡ 60���� ũ�� 
        float TheCheckPoint = 50f;
        float zRotation = transform.eulerAngles.z;
        float xRotation = transform.eulerAngles.x;
        if (zRotation > 180f)
        {
            zRotation -= 360f; // 180�ƺ��� ũ�� ���� ��ȯ
        }
        if (xRotation > 180f)
        {
            xRotation -= 360f; // 180�ƺ��� ũ�� ���� ��ȯ
        }
        //Debug.Log("Z :" + zRotation);
        //if (zRotation < -40f)
        if (xRotation > TheCheckPoint || xRotation < -TheCheckPoint || zRotation > TheCheckPoint || zRotation < -TheCheckPoint)
        {
            CharacterRigidbody.useGravity = true;
            CharacterRigidbody.isKinematic = false;
            //animator.enabled = false;
            //Destroy(hpBarPrefab);
            this.enabled = false;
        }
    }





}
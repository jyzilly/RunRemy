using UnityEngine;


public class RGTCharacterBalanceController : MonoBehaviour
{
    [Header("Default settings")]
    [SerializeField] private Animator animator;
    [SerializeField] Transform TheBall;
    [SerializeField] Rigidbody CharacterRigidbody;
    [SerializeField] private GameObject hpBarPrefab = null;
    //[SerializeField] private RGTHpBar hpBar = null;

    //�� ������ �߽��� ��� ����
    private float balanceHeight = 3.5f;


    private void Start()
    {
        //isKinematic�� true�� �����ϸ� �߷��� ������ ���� �ʰ�, ���� �ڵ�θ� ��ġ/ȸ���� ����
        CharacterRigidbody.isKinematic = true;
        Animator animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //ĳ���Ͱ� �� ���� ������ ���̿� �ֵ��� ����
        transform.position = TheBall.position + Vector3.up * balanceHeight;
    }

    private void Update()
    {
        animator.SetBool("SetLeftActive", true);

        RotateZ(Thedir());
        RotateX(Thedir());

        //ȸ������ �����ϴ� �ɷ� �ٲ�� ��
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateZ(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateZ(-1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateX(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateX(-1);
        }

        CheckRotation();
    }

    //ĳ������ ���� ���⸦ �������� ���� ������ �����Ϸ��� �Լ�
    private float Thedir()
    {
        //ĳ������ ���� ��ġ - �߽���
        Vector3 center = transform.position;
        //ĳ������ �Ӹ� ���� ����
        Vector3 headPoint = transform.up + transform.position;

        Vector3 dir = headPoint - center;
        float a = 0;

        //Atan2�� ����Ͽ� Y-Z ��鿡���� ĳ���� ���� ������ ��� �¿� ����
        float angle = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;

        //���� ��� ���� ���� �����θ� ����
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

    //X��(�յ�)���� ĳ���͸� ȸ����Ű�� �Լ�
    private void RotateX(float _direction)
    {
        transform.Rotate(Vector3.right, _direction * Time.deltaTime * 50f);
    }

    //Z��(�¿�)���� ĳ���͸� ȸ����Ű�� �Լ�
    private void RotateZ(float _direction)
    {
        transform.Rotate(Vector3.forward, _direction * Time.deltaTime * 50f);
    }

    //ȸ�� �� üũ
    private void CheckRotation()
    {
        //���� üũ�ϱ� ���࿡ 60���� ũ�� 
        float TheCheckPoint = 50f;
        float zRotation = transform.eulerAngles.z;
        float xRotation = transform.eulerAngles.x;


        if (zRotation > 180f)
        {
            //180�ƺ��� ũ�� ���� ��ȯ
            zRotation -= 360f; 
        }

        if (xRotation > 180f)
        {
            //180�ƺ��� ũ�� ���� ��ȯ
            xRotation -= 360f; 
        }

        //Debug.Log("Z :" + zRotation);
        //X�� �Ǵ� Z���� ���Ⱑ ������ �Ӱ� ����(TheCheckPoint)�� �Ѿ�� �������� ����
        if (xRotation > TheCheckPoint || xRotation < -TheCheckPoint || zRotation > TheCheckPoint || zRotation < -TheCheckPoint)
        {
            //�߷�Ȱ��ȭ
            CharacterRigidbody.useGravity = true;
            //���� ������ ĳ���͸� �����ϵ��� ��ȯ
            CharacterRigidbody.isKinematic = false;
            //�ִϸ��̼� ����
            animator.enabled = false;
            //hp�� �İ�
            Destroy(hpBarPrefab);
            //��ũ��Ʈ ����
            this.enabled = false;
        }
    }

}
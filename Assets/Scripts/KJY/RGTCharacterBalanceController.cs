using UnityEngine;


public class RGTCharacterBalanceController : MonoBehaviour
{
    [Header("Default settings")]
    [SerializeField] private Animator animator;
    [SerializeField] Transform TheBall;
    [SerializeField] Rigidbody CharacterRigidbody;
    [SerializeField] private GameObject hpBarPrefab = null;
    //[SerializeField] private RGTHpBar hpBar = null;

    //공 위에서 중심을 잡는 높이
    private float balanceHeight = 3.5f;


    private void Start()
    {
        //isKinematic을 true로 설정하면 중력의 영향을 받지 않고, 오직 코드로만 위치/회전을 제어
        CharacterRigidbody.isKinematic = true;
        Animator animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //캐릭터가 공 위의 일정한 높이에 있도록 고정
        transform.position = TheBall.position + Vector3.up * balanceHeight;
    }

    private void Update()
    {
        animator.SetBool("SetLeftActive", true);

        RotateZ(Thedir());
        RotateX(Thedir());

        //회전값을 조절하는 걸로 바꿔야 함
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

    //캐릭터의 현재 기울기를 바탕으로 복원 방향을 결정하려는 함수
    private float Thedir()
    {
        //캐릭터의 현재 위치 - 중심점
        Vector3 center = transform.position;
        //캐릭터의 머리 방향 벡터
        Vector3 headPoint = transform.up + transform.position;

        Vector3 dir = headPoint - center;
        float a = 0;

        //Atan2를 사용하여 Y-Z 평면에서의 캐릭터 기울기 각도를 계산 좌우 기울기
        float angle = Mathf.Atan2(dir.z, dir.y) * Mathf.Rad2Deg;

        //음수 양수 따라 정한 힘으로만 복원
        if (angle > 0)
        {
            a = 0.3f;
        }
        else
        {
            a = -0.3f;
        }
        //Debug.DrawLine(center,headPoint,Color.green);
        //Debug.Log("각도는 " + angle + "a :" + a);
        return a;
    }

    //X축(앞뒤)으로 캐릭터를 회전시키는 함수
    private void RotateX(float _direction)
    {
        transform.Rotate(Vector3.right, _direction * Time.deltaTime * 50f);
    }

    //Z축(좌우)으로 캐릭터를 회전시키는 함수
    private void RotateZ(float _direction)
    {
        transform.Rotate(Vector3.forward, _direction * Time.deltaTime * 50f);
    }

    //회전 값 체크
    private void CheckRotation()
    {
        //각도 체크하기 만약에 60보다 크면 
        float TheCheckPoint = 50f;
        float zRotation = transform.eulerAngles.z;
        float xRotation = transform.eulerAngles.x;


        if (zRotation > 180f)
        {
            //180°보다 크면 음수 변환
            zRotation -= 360f; 
        }

        if (xRotation > 180f)
        {
            //180°보다 크면 음수 변환
            xRotation -= 360f; 
        }

        //Debug.Log("Z :" + zRotation);
        //X축 또는 Z축의 기울기가 설정된 임계 각도(TheCheckPoint)를 넘어서면 떨어지는 설정
        if (xRotation > TheCheckPoint || xRotation < -TheCheckPoint || zRotation > TheCheckPoint || zRotation < -TheCheckPoint)
        {
            //중력활성화
            CharacterRigidbody.useGravity = true;
            //물리 엔진이 캐릭터를 제어하도록 전환
            CharacterRigidbody.isKinematic = false;
            //애니메이숀 정지
            animator.enabled = false;
            //hp바 파과
            Destroy(hpBarPrefab);
            //스크립트 정지
            this.enabled = false;
        }
    }

}
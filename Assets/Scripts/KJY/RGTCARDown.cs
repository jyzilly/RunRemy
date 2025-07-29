using UnityEngine;


//차량의 속도가 일정 기준치 이하로 떨어질 경우 차가 점점 땅에 꺼진다, 연타해서 올려야 한다.

public class RGTCARDown : MonoBehaviour
{
    //최대 가라앉는 깊이
    public float sinkDepth = 2f;
    //가라앉는 속도
    public float sinkSpeed = 0.5f;
    //떠오르는 속도
    public float riseSpeed = 0.3f;
    //연타 감지 시간
    public float requiredTapSpeed = 0.2f;
    //속도 임계값 (60 이하일 때 가라앉음)
    public float speedThreshold = 60f;  

    //원래 위치 저장
    private Vector3 startPos;  
    //목표 위치
    private Vector3 targetPos; 
    private bool isSinking = false;
    private bool isRising = false;
    private float lastTapTime = 0f;
    private Rigidbody rb; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        targetPos = startPos;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Quicksand"))
        {
            //속도 변환
            float currentSpeed = rb.linearVelocity.magnitude * 3.6f; 

            if (currentSpeed < speedThreshold && !isSinking)
            {
                StartSinking();
            }
        }
    }

    void Update()
    {
        if (isSinking)
        {
            if (isRising)
            {
                targetPos += new Vector3(0, riseSpeed * Time.deltaTime, 0);
                //원래 위치 이상 올라가지 않도록 제한
                targetPos.y = Mathf.Min(targetPos.y, startPos.y); 
            }
            else
            {
                targetPos -= new Vector3(0, sinkSpeed * Time.deltaTime, 0);
                //최대 깊이 이하로 못 가게 제한
                targetPos.y = Mathf.Max(targetPos.y, startPos.y - sinkDepth); 
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
        }

        //연타 감지 (Space, W 키)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            //연타 시 상승
            if (Time.time - lastTapTime < requiredTapSpeed) 
            {
                isRising = true;
            }
            lastTapTime = Time.time;
        }

        //차량의 절반 이상이 땅 위에 있으면 이동 가능
        if (transform.position.y >= startPos.y - (sinkDepth / 2))
        {
            //이동 가능 마찰 감소
            rb.linearDamping = 0.1f; 
        }
        else
        {
            //이동 불가능 마찰 증가
            rb.linearDamping = 5f; 
        }
    }

    void StartSinking()
    {
        isSinking = true;
        targetPos = transform.position;
    }
}

using UnityEngine;
using System; // Enum ���� ��� ���
using TMPro;


public class sliderM : MonoBehaviour
{
    // ���¸� ��Ÿ���� enum ���� (�±� �̸��� �ҹ��ڷ� ����ϹǷ� ToLower()�� ��ȯ)
    public enum CollisionState
    {
        Win,
        Fail,
        Pass
    }

    public RectTransform handle;
    private float moveSpot;            // �ڵ��� �̵� ��ġ�� �����ϴ� ����
    private bool movingRight = true;   // �ڵ��� ���������� �̵� ������ ����
    private bool isPaused = false;     // �ڵ��� �̵��� �Ͻ� �����Ǿ����� ����
    public Canvas canvas;
    public CollisionState lastCollisionState = CollisionState.Fail;
    public static event Action OnShutdown;
    int seconds;
    int milliseconds;

    //public TextMeshProUGUI stopwatchText; // UI Text�� �ð��� ǥ���� ����
    private float elapsedTime; // ��� �ð�

    private void Start()
    {
        moveSpot = handle.anchoredPosition.x; // �ڵ��� �ʱ� ��ġ ����
        elapsedTime = 0f;
    }

    private void Update()
    {
       // if (!isPaused) elapsedTime += Time.deltaTime;
        UpdateStopwatchDisplay();
        

        HandleInput(); // ����� �Է� ó��
        if (!isPaused) // �̵��� �Ͻ� ������ �ƴ� �� , isPaused = false;
        {
            MoveHandle(); // �ڵ� �̵�
        }

        
    }

 
    // �����ġ �ð� ǥ�� ������Ʈ
    private void UpdateStopwatchDisplay()
    {
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        milliseconds = Mathf.FloorToInt((elapsedTime * 100) % 100); 
        //stopwatchText.text = seconds.ToString() + ":" + milliseconds.ToString();
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPaused = true;
            if (isPaused) // �̵��� �Ͻ� �����Ǿ��� ��
            {
                lastCollisionState = CheckCollision();  // �浹 üũ (���ο��� ���¿� ���� ó��)
                //Invoke("ShutDown"); // 1�� �Ŀ� canvas ����
                ShutDown();
            }
        }
    }

    private void MoveHandle()
    {
        // Time.deltaTime�� Time.timeScale�� �̿��Ͽ� �̵� �ӵ� ����
        //float moveSpeed = Time.deltaTime * 50;
        //moveSpot += movingRight ? moveSpeed : -moveSpeed; // �̵� ���⿡ ���� ��ǥ ����/����
        moveSpot += Time.deltaTime * 100;


        // �¿� �Ѱ��� üũ (0 ~ 145)
        if (moveSpot >= 145)
        {
            lastCollisionState = CollisionState.Fail; // Fail ��� ����
            ShutDown();
            Debug.Log("Fail��� ���⿡ �ֱ�");

        }


        // �ڵ��� ��ġ ������Ʈ
        handle.anchoredPosition = new Vector2(moveSpot, handle.anchoredPosition.y);
    }

    /// <summary>
    /// �浹�� üũ�ϰ�, �浹�� ���¿� ���� �α׸� ����մϴ�.
    /// </summary>
    public CollisionState CheckCollision()
    {
        string[] tags = { "win", "fail", "pass" };
        foreach (string tag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag); // �ش� �±׸� ���� ��ü���� ã��
            foreach (GameObject obj in objects)
            {
                RectTransform rt = obj.GetComponent<RectTransform>(); // ��ü�� RectTransform�� ������
                if (RectTransformUtility.RectangleContainsScreenPoint(rt, handle.position)) // �ڵ��� ��ü ���� �ִ��� Ȯ��
                {
                    // Debug.Log($"Handle is on {tag} "); // �ڵ��� �ش� �±� ���� ������ �α׿� ���
                    switch (tag)
                    {
                        case "win":
                            Debug.Log("win");
                            return CollisionState.Win;
                        case "fail":
                            Debug.Log("fail");
                            return CollisionState.Fail;
                        case "pass":
                            Debug.Log("pass");
                            return CollisionState.Pass;

                    }

                }
            }
        }
        return CollisionState.Fail;
    }

    private void ShutDown()
    {
       // elapsedTime = 0f;
        isPaused = true;
        moveSpot = 0f;
        canvas.gameObject.SetActive(false);
        OnShutdown?.Invoke();
    }

    // ���߿� �ٽ� ������ �� ����� �Լ�
    public void OpenCanvas()
    {
       // elapsedTime = 0f;
        canvas.gameObject.SetActive(true);
        isPaused = false;

    }
   
}

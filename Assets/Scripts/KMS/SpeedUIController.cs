using TMPro;
using UnityEngine;
using System.Collections;

public class SpeedUIController : MonoBehaviour
{
    [SerializeField] private RGTPoopGaugeManager poo;
    private GameObject player = null;
    private PlayerKMS playerKMS = null;

    // �ӵ� ǥ�ÿ� TMP (��: UI ��ܿ� �ӵ����� ǥ��)
    public TextMeshProUGUI tmp = null;
    // ī��Ʈ�ٿ� ǥ�ÿ� TMP (��: ȭ�� �߾ӿ� "3", "2", "1" ǥ��)
    public TextMeshProUGUI countdownTMP = null;

    public float dangerSpeed = 60f;
    private Color currentColor;

    private bool isCountdownActive = false;
    private Coroutine countdownCoroutine = null;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        //Debug.Log("Player : " + player);
        if (player != null)
        {
            //Debug.Log("�÷��̾� ã��");
            playerKMS = player.GetComponent<PlayerKMS>();
            if(playerKMS != null)
            {
                //Debug.Log("�÷��̾ũ��Ʈ ã��");
            }
        }
        //else
        //{
        //    Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�!");
        //}
    }

    private void Update()
    {
        //Debug.Log("���ǵ� UI ������Ʈ �ǰ� ����");
        // PlayerKMS�� ���������� �Ҵ�Ǿ����� Ȯ��
        if (playerKMS == null)
        {
            //Debug.Log("�÷��̾ ��ã��");
            return;
        }

        //Debug.Log("���ǵ� UI���� player�� ã��");

        // ActiveRigidbody�� ž�� ������Ʈ�� ������ٵ� ������ �װ�, ������ �÷��̾� �ڽ��� ������ٵ�(mainRigidbody)�� ���
        float speed = playerKMS.ActiveRigidbody.linearVelocity.magnitude;
        poo.UpdatePoopGauge(speed);
        tmp.text = speed.ToString("F2");

        // �ӵ��� ���� ������ ���� (���ʿ��� ������Ʈ ����)
        Color newColor = speed <= dangerSpeed ? Color.red : Color.white;
        if (currentColor != newColor)
        {
            tmp.color = newColor;
            currentColor = newColor;
        }
        //Debug.Log("���ǵ� �ؽ�Ʈ ������Ʈ �Ϸ�");

        // �ӵ��� dangerSpeed �����̸� ī��Ʈ�ٿ� ����
        if (speed <= dangerSpeed)
        {
            if (!isCountdownActive && !(playerKMS.currentState == PlayerKMS.PlayerState.Dead))
            {
                countdownCoroutine = StartCoroutine(CountdownToDeath());
            }
        }
        // �ӵ��� dangerSpeed���� ������ ī��Ʈ�ٿ� �ߴ� �� �ʱ�ȭ
        else
        {
            if (isCountdownActive)
            {
                StopCoroutine(countdownCoroutine);
                isCountdownActive = false;
                countdownTMP.text = "";
            }
        }
    }

    private IEnumerator CountdownToDeath()
    {
        isCountdownActive = true;
        // ī��Ʈ�ٿ�: 3, 2, 1 (1�� ����)
        for (int i = 5; i > 0; i--)
        {
            countdownTMP.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownTMP.text = "";

        // 3�ʰ� ������ �÷��̾� ���¸� Dead�� ����
        // PlayerKMS�� public �޼��� SetDeadState() �Ǵ� Die()�� �־�� �մϴ�.
        playerKMS.SetDeadState();

        isCountdownActive = false;
    }


    // �ӵ� �޾ƿͼ� UI�� ������Ʈ

    // �ӵ��� ���� ���Ϸ� �������� �۾��� ���������� �ٲ�

    // ī��Ʈ �ٿ�?( �Ҹ��� °��°�� �Ҹ��� ���ٴ����ؼ� �ӵ��� �������� �����ϴٴ°� ǥ��)
}

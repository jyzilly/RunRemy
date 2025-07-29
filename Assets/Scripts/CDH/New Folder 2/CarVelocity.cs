using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarVelocity : MonoBehaviour
{
    public TextMeshProUGUI speedText;  // �ӵ��� ǥ���� UI Text
    public PlayerKMS playerKMS;

    public Rigidbody rb;
    public RectTransform needle;  // ���̾� ������ ��� �ٴ��� �����̱� ���� RectTransform
    public float maxSpeed = 100f;  // �ִ� �ӵ� ����


    //private void Start()
    //{
    //    rb = playerKMS.currentObjectPrefab.GetComponent<Rigidbody>();
    //}

    private void Update()
    {
        if(playerKMS.currentObjectPrefab != null)
        {
            rb = playerKMS.currentObjectPrefab.GetComponent<Rigidbody>();
        }

        float speed = rb.linearVelocity.magnitude;
        Debug.Log("���ǵ�" + speed);
        //CV.UpdateSpeed(speed)
        speedText.text = "Speed: " + speed.ToString("F2") + " km/h";

        float angle = Mathf.Lerp(0, 180, speed / maxSpeed);
        needle.localRotation = Quaternion.Euler(0, 0, -angle);  // �ٴ� ȸ��

    }
    //public void UpdateSpeed(float speed)
    //{
    //    // �ؽ�Ʈ�� �ӵ� ǥ��

    //    // �ӵ��踦 �ٴ� ���·� ������ ��� �ٴ� ���� ���
    //    //float angle = Mathf.Lerp(0, 180, speed / maxSpeed);
    //    //needle.localRotation = Quaternion.Euler(0, 0, -angle);  // �ٴ� ȸ��
    //}
}


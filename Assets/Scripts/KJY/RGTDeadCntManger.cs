using TMPro;
using UnityEngine;

public class RGTDeadCntManger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deadCnt;

    private void Start()
    {
        TheDeadCnt();
    }

    private void TheDeadCnt()
    {
        //�÷��̾� ���� Ƚ���� �����ͼ� UI�� ���
        string Cnt = PlayerKMS.DeadCnt.ToString();

        deadCnt.text = ("���� Ƚ�� : ") + Cnt;
    }
    
}

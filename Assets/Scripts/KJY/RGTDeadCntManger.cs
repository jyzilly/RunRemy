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
        //플레이어 죽은 횟수를 가져와서 UI로 출력
        string Cnt = PlayerKMS.DeadCnt.ToString();

        deadCnt.text = ("지린 횟수 : ") + Cnt;
    }
    
}

using UnityEngine;
using UnityEngine.AI;

//ȸ�����ٶ� ������ ������ġ�� �̵� �ñ�� ���

public class RGTWaypoint : MonoBehaviour
{
    //������ġ ����
    [SerializeField] private Transform originTrs; 


    //ȸ�����ٶ� ������ ������ġ�� �̵�
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            other.gameObject.transform.position = originTrs.position;
        }
    }


}

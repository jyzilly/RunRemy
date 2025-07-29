using UnityEngine;
using UnityEngine.AI;

//회오리바람 만나면 시작위치로 이동 시기는 기능

public class RGTWaypoint : MonoBehaviour
{
    //시작위치 저장
    [SerializeField] private Transform originTrs; 


    //회오리바람 만나면 시작위치로 이동
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            other.gameObject.transform.position = originTrs.position;
        }
    }


}

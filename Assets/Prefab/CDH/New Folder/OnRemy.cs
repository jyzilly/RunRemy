using UnityEngine;

public class OnRemy : MonoBehaviour
{
    public GameObject SitRemy;
    public GameObject RunRemy;


    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("�÷��̾� �� ����");
        if(other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ����");
            RunRemy.SetActive(false);
            SitRemy.SetActive(true);
        }
    }

}

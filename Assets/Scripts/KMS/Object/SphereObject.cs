using UnityEngine;
using UnityEngine.Purchasing;

public class SphereObject : InteractableObject
{
    [SerializeField] private Vector3 velocity;           // ���� �ӵ�
    [SerializeField] private float forceValue = 10f;
    
    public float durabilityLossRate = 0.1f;
    public bool RockActive = false;

    void Start()
    {
        velocity = Vector3.zero; // �ʱ� �ӵ� ����
    }

    private void Update()
    {
        // ���� �����Ͽ� �ӵ� ����
        velocity += Vector3.forward * forceValue * Time.deltaTime;

        if (RockActive == true)
        {
            Mdurability();
            //Debug.Log("currentDurability"+ currentDurability);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            RockActive = true;
        }
    }

    private void Mdurability()
    {

        //Debug.Log("Here : " + velocity.magnitude);
        // �����̴� ���� ������ ����
        if (velocity.magnitude > 0.1f)
        {
            currentDurability -= durabilityLossRate * Time.deltaTime;
        }
        if(currentDurability <= 0)
        {
            RockActive = false;
            Debug.Log("false");
        }
    }

}

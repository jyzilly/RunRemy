using UnityEngine;

public class ArrowKey : MonoBehaviour
{

    [SerializeField] private float balanceSpeed = 100f;


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            RotateZ(1);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            RotateZ(-1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {

            RotateX(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            RotateX(-1);
        }
        else
        {
            RanDomRotation();
        }
    }

    private void RotateX(float _direction)
    {
        float xRotation = _direction * balanceSpeed * Time.deltaTime;

        float newXRotation = transform.eulerAngles.x + xRotation;

        //����Ƽ�� EulerAngles�� 0~360���� ǥ���ǹǷ� -60~60���� ��ȯ�ؾ� ��
        if (newXRotation > 180f)
            newXRotation -= 360f;

        newXRotation = Mathf.Clamp(newXRotation, -60f, 60f);

        transform.rotation = Quaternion.Euler(newXRotation, 0f, 0f);
    }    

    private void RotateZ(float _direction)
    {
        float zRotation = _direction * balanceSpeed * Time.deltaTime;

        float newZRotation = transform.eulerAngles.z + zRotation;

        //����Ƽ�� EulerAngles�� 0~360���� ǥ���ǹǷ� -60~60���� ��ȯ�ؾ� ��
        if (newZRotation > 180f)
            newZRotation -= 360f;

        newZRotation = Mathf.Clamp(newZRotation, -60f, 60f);

        transform.rotation = Quaternion.Euler(0f, 0f, newZRotation);
    }

    private void RanDomRotation()
    {

        float randomRotation = Random.Range(-90f, 90f) * Time.deltaTime;
        transform.Rotate(0, randomRotation, randomRotation);

    }
}

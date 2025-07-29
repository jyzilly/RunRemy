using UnityEngine;

public class RGTTire : MonoBehaviour
{

    [SerializeField] private GameObject TireRagRemy;
    [SerializeField] private GameObject Tire_1;
    [SerializeField] private GameObject RemyOri;


    [SerializeField] private Rigidbody Object;

    [SerializeField] private Transform Neck;
    [SerializeField] private Transform NeckPos;

    private Vector3 originNeckV2;

    private void Start()
    {

        originNeckV2 = Neck.transform.position;
    }

    private void Update()
    {
        RandomValue();

        FollowThePos();
        Rotatey(1*8);
        Checkposition();

        //Debug.Log("z: " + transform.localPosition.z);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Posx(-1);

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Posx(1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Posz(1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {

            Posz(-1);
        }
    }

    private void Rotatey(float _direction)
    {
        transform.Rotate(Vector3.up, _direction * Time.deltaTime * 200f);
    }

    private void Posx(float _dir)
    {
        //transform.Translate(Vector3.right * _dir* Time.deltaTime);
        transform.Translate(Vector3.right * _dir * Time.deltaTime * 1f, Space.World);

    }    
    private void Posz(float _dir)
    {
        //transform.Translate(Vector3.right * _dir* Time.deltaTime);
        transform.Translate(Vector3.forward * _dir * Time.deltaTime * 1f, Space.World);

    }

    private void RandomValue()
    {
        int a = Random.Range(1, 4);
        if(a == 1)
        {
            Posx(-1);
        }
        else if (a == 2)
        {
            Posx(1);
        }
        else if(a == 3)
        {
            Posz(-1);
        }
        else if(a == 4)
        {
            Posz(1);
        }
    }

    private void FollowThePos()
    {
        Neck.transform.position = NeckPos.transform.position;
    }

    private void Checkposition()
    {
        Vector3 Offset = Neck.transform.position - originNeckV2;
        float Length = Offset.magnitude;
        Debug.Log("Length : " + Length);

        float Limit = 0.5f;

        if(Length >= Limit)
        {
            TireRagRemy.SetActive(true);
            Tire_1.SetActive(true);
            Destroy(RemyOri);
            this.enabled = false;

        }

        
    }




}

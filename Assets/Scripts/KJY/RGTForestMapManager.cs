using UnityEngine;


//���ʿ��� Ư�� ����(�÷��̾� ���)�� �����Ǿ��� ��, ���� ȯ��(��ī�̹ڽ�, ����)�� �ε巴�� ��ȯ

public class RGTForestMapManager : MonoBehaviour
{
    public PlayerKMS player;

    [SerializeField] private GameObject ForestMap;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //�⺻ ����
    [SerializeField] private Light directionalLight;
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private Color MorningColor = new Color(1.0f, 0.93f, 0.8f);
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private float MorningIntensity = 1.5f;
    [SerializeField] private Vector3 MorningRotation = new Vector3(30f, 200f, 0f);
    //�ε巯�� ��ȯ �ӵ�
    [SerializeField] private float transitionSpeed = 1.5f;

    //�⺻ ȯ�� ���� �����ϱ� ���� ������
    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //ȯ�� ��ȯ ������ �����ϱ� ���� ����
    private bool isInZone = false; //��ȯ ����
    private float blendFactor = 0f; //0�� 1 ���̸� �����Ͽ� ��ȯ�� ������ ��Ÿ���� ��


    private void Start()
    {
        //SkyBox �⺻�� ����
        defaultSkybox = RenderSettings.skybox;

        //����Ʈ ���� ������ ����
        if (directionalLight)
        {
            defaultLightColor = directionalLight.color;
            defaultLightIntensity = directionalLight.intensity;
            defaultLightRotation = directionalLight.transform.rotation;
        }
    }


    private void Update()
    {
        //�÷��̾� ����ϸ�
        if (player.currentState == PlayerKMS.PlayerState.Dead)
        {
            ChangeTheSkyBox();
            ForestMap.SetActive(true);
        }

        //isInZone���� ���� blendFactor�� �ε巴�� 0 �Ǵ� 1�� ����
        if (isInZone)
        {
            //��ǥ ��(1)�� ���� �ε巴�� ����
            blendFactor = Mathf.Lerp(blendFactor, 1f, Time.deltaTime * transitionSpeed);
        }
        else
        {
            //�ʱ� ��(0)�� ���� �ε巴�� ����
            blendFactor = Mathf.Lerp(blendFactor, 0f, Time.deltaTime * transitionSpeed);
        }

        //���� ��ȯ
        if (directionalLight)
        {
            directionalLight.color = Color.Lerp(defaultLightColor, MorningColor, blendFactor);
            directionalLight.intensity = Mathf.Lerp(defaultLightIntensity, MorningIntensity, blendFactor);

            Quaternion targetRotation = Quaternion.Euler(MorningRotation);
            directionalLight.transform.rotation = Quaternion.Lerp(defaultLightRotation, targetRotation, blendFactor);
        }
    }


    private void ChangeTheSkyBox()
    {
        RenderSettings.skybox = newSkybox;
        DynamicGI.UpdateEnvironment();
        isInZone = true;
    }
}

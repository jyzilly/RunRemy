using UnityEngine;

public class RGTSnowMapManager : MonoBehaviour
{
    [SerializeField] private GameObject ForestMap;
    [SerializeField] private GameObject DesertMap;
    [SerializeField] private GameObject SnowMap;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //�⺻ ����
    [SerializeField] private Light directionalLight;
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private Color NightColor = new Color(0.6f, 0.45f, 0.8f);
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private float NightIntensity = 1.2f;
    [SerializeField] private Vector3 NightRotation = new Vector3(30f, 200f, 0f);
    //�ε巯�� ��ȯ �ӵ�
    [SerializeField] private float transitionSpeed = 1.5f; 

    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //ȯ�� ��ȯ ������ �����ϱ� ���� ����
    private bool isInZone = false;
    private float blendFactor = 0f;


    private void Start()
    {
        //SkyBox
        defaultSkybox = RenderSettings.skybox;

        if (directionalLight)
        {
            defaultLightColor = directionalLight.color;
            defaultLightIntensity = directionalLight.intensity;
            defaultLightRotation = directionalLight.transform.rotation;
        }
    }


    private void Update()
    {
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
            directionalLight.color = Color.Lerp(defaultLightColor, NightColor, blendFactor);
            directionalLight.intensity = Mathf.Lerp(defaultLightIntensity, NightIntensity, blendFactor);

            Quaternion targetRotation = Quaternion.Euler(NightRotation);
            directionalLight.transform.rotation = Quaternion.Lerp(defaultLightRotation, targetRotation, blendFactor);
        }
    }


    //�ʰ�� ������ �� ���� ������ Ȱ��ȭ ��Ű�� ���� ���� ��Ȱ��ȭ ��Ų��. �׸��� �ش� ���ʿ� �ִ� ȯ���� Ȱ��ȭ��Ų��.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            ForestMap.SetActive(false);
            DesertMap.SetActive(false);
            SnowMap.SetActive(true);
            RenderSettings.fog = false;


            //SkyBox
            RenderSettings.skybox = newSkybox;
            DynamicGI.UpdateEnvironment();
            isInZone = true;
        }
    }



}

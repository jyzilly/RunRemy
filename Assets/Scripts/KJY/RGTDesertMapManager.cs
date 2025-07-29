using UnityEngine;

public class RGTDesertMapManager : MonoBehaviour
{
    [SerializeField] private GameObject DesertMap;
    [SerializeField] private GameObject ForestMap;
    [SerializeField] private GameObject SnowMap;

    //���� �Ȱ�
    [SerializeField] private Color fogColor = new Color(0.6f, 0.4f, 0.2f, 1f); 
    //��ǥ �Ȱ� ��
    [SerializeField] private float targetFogDensity = 0.01f;
    //�ε巯�� ��ȯ �ӵ�
    [SerializeField] private float transitionSpeed = 1.5f; 

    private Color defaultFogColor;
    private float defaultFogDensity;
    private bool isInFogZone = false;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //�⺻ ����
    [SerializeField] private Light directionalLight;
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private Color afternoonLightColor = new Color(1.0f, 0.75f, 0.5f);
    //������ ��ȯ�� ��ǥ ���� ��
    [SerializeField] private float afternoonLightIntensity = 1.8f;
    [SerializeField] private Vector3 afternoonLightRotation = new Vector3(30f, 200f, 0f);

    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //ȯ�� ��ȯ ������ �����ϱ� ���� ����
    private bool isInZone = false;
    private float blendFactor = 0f;

    void Start()
    {
        RenderSettings.fog = false;



        //SkyBox �⺻�� ����
        defaultSkybox = RenderSettings.skybox;

        if (directionalLight)
        {
            defaultLightColor = directionalLight.color;
            defaultLightIntensity = directionalLight.intensity;
            defaultLightRotation = directionalLight.transform.rotation;
        }
    }

    void Update()
    {
        if (isInFogZone)
        {
            //�ε巴�� �Ȱ� ����
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, fogColor, Time.deltaTime * transitionSpeed);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, targetFogDensity, Time.deltaTime * transitionSpeed);
        }
        else
        {
            //�⺻ ���·� ����
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, defaultFogColor, Time.deltaTime * transitionSpeed);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, defaultFogDensity, Time.deltaTime * transitionSpeed);

            if (Mathf.Abs(RenderSettings.fogDensity - defaultFogDensity) < 0.001f)
            {
                RenderSettings.fog = defaultFogDensity > 0;
            }
        }

        //SkyBox
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

        // Skybox ��ȯ
        //RenderSettings.skybox.Lerp(defaultSkybox, newSkybox, blendFactor);
        //DynamicGI.UpdateEnvironment();

        //���� ��ȯ
        if (directionalLight)
        {
            directionalLight.color = Color.Lerp(defaultLightColor, afternoonLightColor, blendFactor);
            directionalLight.intensity = Mathf.Lerp(defaultLightIntensity, afternoonLightIntensity, blendFactor);

            Quaternion targetRotation = Quaternion.Euler(afternoonLightRotation);
            directionalLight.transform.rotation = Quaternion.Lerp(defaultLightRotation, targetRotation, blendFactor);
        }


    }

    //�� ��谪 �������� �縷�� Ȱ��ȭ��Ű�� ������ ���� ��Ȱ��ȭ��Ų��, �׸��� �縷�� �´� ������ Ȱ��ȭ ��Ų��.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("carbody"))
        {
            DesertMap.SetActive(true);
            isInFogZone = true;
            ForestMap.SetActive(false);
            SnowMap.SetActive(false);

            //SkyBox
            RenderSettings.skybox = newSkybox; 
            DynamicGI.UpdateEnvironment();
            isInZone = true;

        }
    }

}

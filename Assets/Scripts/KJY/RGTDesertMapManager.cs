using UnityEngine;

public class RGTDesertMapManager : MonoBehaviour
{
    [SerializeField] private GameObject DesertMap;
    [SerializeField] private GameObject ForestMap;
    [SerializeField] private GameObject SnowMap;

    //갈색 안개
    [SerializeField] private Color fogColor = new Color(0.6f, 0.4f, 0.2f, 1f); 
    //목표 안개 농도
    [SerializeField] private float targetFogDensity = 0.01f;
    //부드러운 전환 속도
    [SerializeField] private float transitionSpeed = 1.5f; 

    private Color defaultFogColor;
    private float defaultFogDensity;
    private bool isInFogZone = false;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //기본 조명
    [SerializeField] private Light directionalLight;
    //조명이 전환될 목표 색상 값
    [SerializeField] private Color afternoonLightColor = new Color(1.0f, 0.75f, 0.5f);
    //조명이 전환될 목표 강도 값
    [SerializeField] private float afternoonLightIntensity = 1.8f;
    [SerializeField] private Vector3 afternoonLightRotation = new Vector3(30f, 200f, 0f);

    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //환경 전환 로직을 제어하기 위한 변수
    private bool isInZone = false;
    private float blendFactor = 0f;

    void Start()
    {
        RenderSettings.fog = false;



        //SkyBox 기본값 저장
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
            //부드럽게 안개 변경
            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, fogColor, Time.deltaTime * transitionSpeed);
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, targetFogDensity, Time.deltaTime * transitionSpeed);
        }
        else
        {
            //기본 상태로 복귀
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
            //목표 값(1)을 향해 부드럽게 증가
            blendFactor = Mathf.Lerp(blendFactor, 1f, Time.deltaTime * transitionSpeed);
        }
        else
        {
            //초기 값(0)을 향해 부드럽게 감소
            blendFactor = Mathf.Lerp(blendFactor, 0f, Time.deltaTime * transitionSpeed);
        }

        // Skybox 전환
        //RenderSettings.skybox.Lerp(defaultSkybox, newSkybox, blendFactor);
        //DynamicGI.UpdateEnvironment();

        //조명 전환
        if (directionalLight)
        {
            directionalLight.color = Color.Lerp(defaultLightColor, afternoonLightColor, blendFactor);
            directionalLight.intensity = Mathf.Lerp(defaultLightIntensity, afternoonLightIntensity, blendFactor);

            Quaternion targetRotation = Quaternion.Euler(afternoonLightRotation);
            directionalLight.transform.rotation = Quaternion.Lerp(defaultLightRotation, targetRotation, blendFactor);
        }


    }

    //맵 경계값 지나가면 사막맵 활성화시키고 지나간 맵을 비활성화시킨다, 그리고 사막에 맞는 설정을 활성화 시킨다.
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

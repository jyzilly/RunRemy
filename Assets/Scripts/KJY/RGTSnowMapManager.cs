using UnityEngine;

public class RGTSnowMapManager : MonoBehaviour
{
    [SerializeField] private GameObject ForestMap;
    [SerializeField] private GameObject DesertMap;
    [SerializeField] private GameObject SnowMap;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //기본 조명
    [SerializeField] private Light directionalLight;
    //조명이 전환될 목표 색상 값
    [SerializeField] private Color NightColor = new Color(0.6f, 0.45f, 0.8f);
    //조명이 전환될 목표 강도 값
    [SerializeField] private float NightIntensity = 1.2f;
    [SerializeField] private Vector3 NightRotation = new Vector3(30f, 200f, 0f);
    //부드러운 전환 속도
    [SerializeField] private float transitionSpeed = 1.5f; 

    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //환경 전환 로직을 제어하기 위한 변수
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
        //isInZone값에 따라 blendFactor를 부드럽게 0 또는 1로 변경
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

        //조명 전환
        if (directionalLight)
        {
            directionalLight.color = Color.Lerp(defaultLightColor, NightColor, blendFactor);
            directionalLight.intensity = Mathf.Lerp(defaultLightIntensity, NightIntensity, blendFactor);

            Quaternion targetRotation = Quaternion.Euler(NightRotation);
            directionalLight.transform.rotation = Quaternion.Lerp(defaultLightRotation, targetRotation, blendFactor);
        }
    }


    //맵경계 도착할 때 전에 숲맵을 활성화 시키고 지난 맵을 비활성화 시킨다. 그리고 해당 숲맵에 있는 환경요소 활성화시킨다.
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

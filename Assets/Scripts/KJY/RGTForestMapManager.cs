using UnityEngine;


//숲맵에서 특정 조건(플레이어 사망)이 충족되었을 때, 맵의 환경(스카이박스, 조명)을 부드럽게 전환

public class RGTForestMapManager : MonoBehaviour
{
    public PlayerKMS player;

    [SerializeField] private GameObject ForestMap;



    //SkyBox
    [SerializeField] private Material newSkybox;
    //기본 조명
    [SerializeField] private Light directionalLight;
    //조명이 전환될 목표 색상 값
    [SerializeField] private Color MorningColor = new Color(1.0f, 0.93f, 0.8f);
    //조명이 전환될 목표 강도 값
    [SerializeField] private float MorningIntensity = 1.5f;
    [SerializeField] private Vector3 MorningRotation = new Vector3(30f, 200f, 0f);
    //부드러운 전환 속도
    [SerializeField] private float transitionSpeed = 1.5f;

    //기본 환경 값을 저장하기 위한 변수들
    private Material defaultSkybox;
    private Color defaultLightColor;
    private float defaultLightIntensity;
    private Quaternion defaultLightRotation;
    //환경 전환 로직을 제어하기 위한 변수
    private bool isInZone = false; //전환 상태
    private float blendFactor = 0f; //0과 1 사이를 보간하여 전환의 정도를 나타내는 값


    private void Start()
    {
        //SkyBox 기본값 저장
        defaultSkybox = RenderSettings.skybox;

        //라이트 관련 값들이 저장
        if (directionalLight)
        {
            defaultLightColor = directionalLight.color;
            defaultLightIntensity = directionalLight.intensity;
            defaultLightRotation = directionalLight.transform.rotation;
        }
    }


    private void Update()
    {
        //플레이어 사망하면
        if (player.currentState == PlayerKMS.PlayerState.Dead)
        {
            ChangeTheSkyBox();
            ForestMap.SetActive(true);
        }

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

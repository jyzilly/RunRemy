using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenM : MonoBehaviour
{
    public Animator animator;
    public Camera camera;
    public float rotationSpeed = 1.0f; // ȸ�� �ӵ� ����
    public GameObject firstRemy;
    public RawImage img1;
    public RawImage img2;
    AnimatorStateInfo stateInfo;
    bool isDestroy = false;

    public GameObject walkingRm;
    public GameObject door;


    public RawImage bubble;
    public RawImage poo;

    public GameObject particle;
    public GameObject toilet;

    public AudioSource audioSource;
    public AudioClip audioClip;

    bool isCheck1 = false;

    private void Awake()
    {

    }
    private void Start()
    {

    }
    private void Update()
    {
        if (!isDestroy)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0)) // �ִϸ��̼� �� Ȯ��
            {
                //Debug.Log("�ִϸ��̼��� ��");
                StartCoroutine(SmoothCameraRotation(new Vector3(0f, -62.932f, 0f), 1.5f)); // ��ǥ ȸ������ �ð� ����
                if (!isCheck1)
                {
                    StartCoroutine(DestroyObj());
                    
                }
                
            }

        }
    }

    private IEnumerator SmoothCameraRotation(Vector3 targetRotation, float duration)
    {
        Quaternion startRotation = camera.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float time = 0;

        while (time < duration)
        {
            camera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
            camera.transform.rotation = endRotation; // ���� ����

    }

    private IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(2f);
        isDestroy = true;
        isCheck1 = true;
        Destroy(firstRemy);
        Destroy(img1);
        Destroy(img2);
        Invoke("Bubble", 0.1f);
        Destroy(toilet, 0.7f);
        Invoke("playParticle",0.7f);
        Invoke("playParticle",0.7f);
        StartCoroutine(PlayAndWait(audioClip));
        Invoke("SoundMn", 0.6f);
    }

    private void Bubble()
    {
        walkingRm.SetActive(true);

        bubble.gameObject.SetActive(true);
        poo.gameObject.SetActive(true);
    }

    private void playParticle()
    {
        particle.SetActive(true);
    }

    private void SoundMn()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    IEnumerator PlayAndWait(AudioClip audioClip)
    {
        yield return new WaitForSeconds(1.3f);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioClip);
        }
        yield return new WaitForSeconds(0.1f);
        SceneMn();
    }

    private void SceneMn()
    {
        SceneManager.LoadScene("Main");    
    }
}

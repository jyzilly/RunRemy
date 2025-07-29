using UnityEngine;

public class EndCheck : MonoBehaviour
{
    public GameObject EndCutScene;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "carbody")
        {
            EndCutScene.SetActive(true);

            PlayerPrefs.SetInt("GameCleared", 1); //���� �� �� UI������ ���ؼ� ����
            PlayerPrefs.Save(); // ����
        }
    }
}

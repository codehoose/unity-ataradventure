using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneAction : MonoBehaviour
{
    public string _targetScene;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(
            new UnityEngine.Events.UnityAction(() =>
            {
                SceneManager.LoadScene(_targetScene);
            }));
    }
}

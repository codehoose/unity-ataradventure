using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadScene : MonoBehaviour
{
    public float _waitTime = 5f;
    public string _targetScene;

    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(_targetScene);
    }
}

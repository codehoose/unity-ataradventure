using System.Collections;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameController _gameController;
    public Locomotion _locomotion;
    public string _requiredObject = "chalice";
    public CanvasGroup _group;

    void Awake()
    {
        _gameController.RoomChanged += (o, e) =>
        {
            if (_gameController.RoomIndex == 1 &&
                _gameController.CurremtItem == _requiredObject)
            {
                _gameController.GameOver();
                _locomotion.GameOver();
                StartCoroutine(FadeUpSign());
            }
        };
    }

    IEnumerator FadeUpSign()
    {
        var alpha = 0f;
        while (alpha < 1f)
        {
            _group.alpha = alpha;
            alpha += Time.deltaTime / 2;

            yield return null;
        }

        _group.alpha = 1;
        yield return GetComponent<AutoLoadScene>().LoadScene();
    }
}

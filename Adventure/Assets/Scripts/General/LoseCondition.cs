using System.Collections;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    private bool _showingMessage = false;

    public CanvasGroup _group;
    public Locomotion _locomotion;
    public GameController _gameController;

    void Update()
    {
        if (_showingMessage) return;

        if (_locomotion.Health == 0)
        {
            _showingMessage = true;
            _locomotion.GameOver();
            _gameController.GameOver(true);
            StartCoroutine(FadeUpSign());
        }
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

using System.Collections;
using UnityEngine;

public class GateActivator : MonoBehaviour
{
    private static readonly float MAX_Y = 3.5f;

    public string key;

    public Transform gate;

    private bool _isActivated = false;

    public void OpenGate()
    {
        if (_isActivated) return;
        GetComponent<BoxCollider2D>().enabled = false;
        _isActivated = true;
        StartCoroutine(ActivateGate());
    }

    public void Reset(Vector2 position, string keyName)
    {
        transform.position = position;
        key = keyName;

        gate.transform.localPosition = Vector3.zero;
        _isActivated = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator ActivateGate()
    {
        var time = 0f;
        while (time < 1f)
        {
            var y = Mathf.Lerp(0, MAX_Y, time);
            gate.transform.localPosition = new Vector2(0, y);
            time += Time.deltaTime / 2f;
            yield return null;
        }

        transform.position = new Vector2(-50, 0);
    }
}

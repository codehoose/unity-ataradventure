using System.Collections;
using UnityEngine;

public class Chalice : MonoBehaviour
{
    public RoomRenderer _renderer;
    private int _count;

    IEnumerator Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _count++;
            if (_count == 6)
            {
                _count++;
            }
            _count %= _renderer._roomColours.Length;
            spriteRenderer.color = _renderer._roomColours[_count];
        }
    }
}

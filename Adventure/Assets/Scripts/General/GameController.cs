using UnityEngine;

public class GameController : MonoBehaviour
{
    private BoxCollider2D _collider;
    private PlayerBoundsDetect _playerBoundsDetect;
    public Transform _player;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _playerBoundsDetect = new PlayerBoundsDetect(_collider, _player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerBoundsDetect.HandleEvent(collision);
    }
}

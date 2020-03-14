using UnityEngine;

class PlayerBoundsDetect
{
    private readonly BoxCollider2D _collider;
    private readonly Transform _player;

    public PlayerBoundsDetect(BoxCollider2D collider, Transform player)
    {
        _collider = collider;
        _player = player;
    }

    public void HandleEvent(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            return;
        }

        var horizontal = Vector3.Cross(Vector3.up, collision.transform.position.normalized);
        if (Mathf.Abs(horizontal.z) > 0.88f)
        {
            var direction = Mathf.Sign(horizontal.z);
            var offset = _collider.bounds.extents.x * 2f * direction;
            _player.position += new Vector3(offset, 0);
            return;
        }

        var vertical = Vector3.Cross(Vector3.right, collision.transform.position.normalized);
        if (Mathf.Abs(vertical.z) > 0f)
        {
            var direction = -Mathf.Sign(vertical.z);
            var offset = _collider.bounds.extents.y * 2f * direction;
            _player.position += new Vector3(0, offset);
        }
    }
}
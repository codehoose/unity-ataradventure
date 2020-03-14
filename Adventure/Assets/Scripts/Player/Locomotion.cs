using UnityEngine;

public class Locomotion : MonoBehaviour
{
    [Header("Locomotion Variables")]
    public float _speed = 20;

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        var y = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        var offset = new Vector2();

        if (Mathf.Abs(x) > 0)
        {
            offset += new Vector2(x, 0);
        }

        if (Mathf.Abs(y) > 0)
        {
            offset += new Vector2(0, y);
        }

        var pos = new Vector2(transform.position.x, transform.position.y);
        GetComponent<Rigidbody2D>().MovePosition(pos + offset);
    }
}

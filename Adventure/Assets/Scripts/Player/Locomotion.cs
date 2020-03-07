using UnityEngine;

public class Locomotion : MonoBehaviour
{
    [Header("Locomotion Variables")]
    public float _speed = 20;

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
        var y = Input.GetAxis("Vertical") * _speed * Time.deltaTime;

        if (Mathf.Abs(x) > 0)
        {
            transform.position += new Vector3(x, 0);
        }

        if (Mathf.Abs(y) > 0)
        {
            transform.position += new Vector3(0, y);
        }
    }
}

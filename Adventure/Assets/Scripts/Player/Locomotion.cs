using UnityEngine;

public class Locomotion : MonoBehaviour
{
    [Header("Locomotion Variables")]
    public float _speed = 20;

    public event GateOpenedEventHandler GateOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check to see who entered, if it's the player
        if (collision.tag != "Gate") return;
        // check that they have the right key and that
        var inventory = GetComponent<Inventory>();
        var gate = collision.GetComponent<GateActivator>();
        if (inventory == null || inventory.CurrentItem == null) return;

        if (inventory.CurrentItem.keyName == gate.key)
        {
            GateOpened?.Invoke(this, new GateOpenedEventArgs());
            gate.OpenGate();
        }
    }

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

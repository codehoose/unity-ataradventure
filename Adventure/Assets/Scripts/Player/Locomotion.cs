using System;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    [Header("Locomotion Variables")]
    public float _speed = 20;

    private bool _gameOver = false;

    public int Health { get; internal set; } = 100;

    public event GateOpenedEventHandler GateOpened;

    public event EventHandler DragonBite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DragonMouth")
        {
            DragonBite?.Invoke(this, EventArgs.Empty);
            return;
        }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "DragonMouth")
        {
            Health--;
            if (Health < 0) Health = 0;
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }

    void Update()
    {
        if (_gameOver) return;

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

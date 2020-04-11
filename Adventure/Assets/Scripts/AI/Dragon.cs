using UnityEngine;

public class Dragon : MonoBehaviour
{
    enum DragonState
    {
        Waiting,
        Chasing,
        Dead
    }

    private bool _isAlive = true;
    private DragonState _state = DragonState.Waiting;
    private Transform _target;
    private GameController _controller;

    [HideInInspector]
    public int _currentRoom = -1;

    public GameObject _alive;
    public GameObject _dead;

    [SerializeField]
    public Color _colour;

    public string _name;

    public float _speed = 8;

    public int _health = 100;

    void Awake()
    {
        _alive.GetComponent<SpriteRenderer>().color = _colour;
        _dead.GetComponent<SpriteRenderer>().color = _colour;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_isAlive && collision.tag == "Pickup")
        {
            var pickup = collision.GetComponent<Pickup>();
            if (pickup != null && pickup.keyName == "sword")
            {
                _health--;
                if (_health <= 0)
                {
                    _state = DragonState.Dead;
                    _controller?.DragonDead();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAlive && collision.tag == "Pickup")
        {
            var pickup = collision.GetComponent<Pickup>();
            if (pickup != null && pickup.keyName == "sword")
            {
                _controller?.DragonHit();
            }
        }
    }

    void Update()
    {
        if (!_isAlive) return;

        switch(_state)
        {
            case DragonState.Chasing:
                if (_target != null)
                {
                    var pos = Vector3.Lerp(transform.position, _target.position, Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(pos);
                }
                break;
            case DragonState.Dead:
                _isAlive = false;
                _alive.SetActive(false);
                _dead.SetActive(true);
                break;
        }
    }

    internal void StartChase(GameController gameController, int currentRoom)
    {
        if (!_isAlive) return;

        _state = DragonState.Chasing;
        _target = gameController._player;
        _currentRoom = currentRoom;
        _controller = gameController;

        // Handle player leaving the screen
        gameController.PlayerLeftScreen += (o, e) =>
          {
              if (_currentRoom == gameController.RoomIndex)
              {
                  return;
              }

              _currentRoom = -1;

              switch(e.Direction)
              {
                  case Direction.East:
                      transform.position += new Vector3(-19.5f, 0) * 2;
                      break;
                  case Direction.West:
                      transform.position += new Vector3(19.5f, 0) * 2;
                      break;
                  case Direction.North:
                      transform.position += new Vector3(0, -10.7f) * 2;
                      break;
                  case Direction.South:
                      transform.position += new Vector3(0, 10.7f) * 2;
                      break;
              }
          };
    }
}

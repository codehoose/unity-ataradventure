using UnityEngine;

public class ScreenID : MonoBehaviour
{

    public GameController _gameController;
    private TMPro.TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _gameController.RoomChanged += (o, e) =>
          {
              _text.text = $"Room ID {_gameController.RoomIndex}";
          };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

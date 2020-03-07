using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Pickup _currentItem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickup")
        {
            var newItem = collision.gameObject.GetComponent<Pickup>();
            if (newItem == _currentItem)
            {
                return;
            }
            else
            {
                PickUp(newItem);
            }
        }
    }

    public void PickUp(Pickup pickup)
    {
        if (_currentItem == pickup)
        {
            return;
        }

        if (_currentItem != null)
        {
            _currentItem.gameObject.transform.SetParent(null);
        }

        _currentItem = pickup;
        _currentItem.gameObject.transform.SetParent(transform);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _currentItem != null)
        {
            _currentItem.gameObject.transform.SetParent(null);
            _currentItem = null;
        }
    }
}

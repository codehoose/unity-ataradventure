using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip _pickup;
    public AudioClip _drop;
    public AudioClip _dragonDead;
    public AudioClip _playerDead;
    public AudioClip _dragonBite;
    public AudioClip _swordHit;

    public AudioSource _source;

    internal void PlayDrop()
    {
        _source.PlayOneShot(_drop);
    }

    internal void PlayPickup()
    {
        _source.PlayOneShot(_pickup);
    }

    internal void PlayDragonDead()
    {
        _source.PlayOneShot(_dragonDead);
    }

    internal void PlayPlayerDead()
    {
        _source.PlayOneShot(_playerDead);
    }

    internal void PlayDragonBite()
    {
        _source.PlayOneShot(_dragonBite);
    }

    internal void PlaySwordHit()
    {
        _source.PlayOneShot(_swordHit);
    }
}

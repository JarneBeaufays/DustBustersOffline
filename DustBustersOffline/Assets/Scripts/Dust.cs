using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    [SerializeField] List<Sprite> _sprites = new List<Sprite>();

    bool _pickedUp = false;

    private void Start()
    {
        if(_sprites.Count == 0) 
        {
            Debug.LogError("Dust::Start() -> No sprites given.");
            return;
        }

        // Change the sprite of the dust particle to a random one
        GetComponentInChildren<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Count)];        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_pickedUp) return;

        if (other.tag == "Player")
        {
            _pickedUp = true;

            Player p = other.gameObject.GetComponent<Player>();
            p.DustPickedUp(1);

            ParticleSystem ps = this.GetComponentInChildren<ParticleSystem>();
            ps.Play(true);

            GetComponent<AudioSource>().Play();

            this.GetComponentInChildren<SpriteRenderer>().enabled = false;

            Destroy(this.gameObject, ps.main.duration);
        }
        if (other.tag == "Huisstofmijt")
        {
            _pickedUp = true;

            Huisstofmijt mijt = other.GetComponent<Huisstofmijt>();
            mijt.ReachedDust();
            mijt.Munch();
            AudioSource audios = other.GetComponent<AudioSource>();
            audios.Play();
            Destroy(this.gameObject, audios.time);
        }
    }
}

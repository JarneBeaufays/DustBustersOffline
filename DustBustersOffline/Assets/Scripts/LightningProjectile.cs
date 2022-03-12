using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _impactParticle = null;
    [SerializeField] private float _movementspeed = 0.5f;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _damage = 3;

    private GameObject _shooter = null;
    private AudioSource _audioSource = null;

    public GameObject Shooter { set { _shooter = value; } }

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(this.transform.forward * _movementspeed);
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject == _shooter) return;

            Instantiate(_impactParticle, this.transform.position, Quaternion.identity);
            Player p = other.gameObject.GetComponent<Player>();
            p.TakeDustOff(_damage);
        }
        else if (other.GetComponent<Huisstofmijt>()) 
        {
            Instantiate(_impactParticle, this.transform.position, Quaternion.identity);
            GameObject.FindObjectOfType<HuisstofmijtManager>().SpawnMijt();
            Destroy(other.gameObject);
        }
    }
}

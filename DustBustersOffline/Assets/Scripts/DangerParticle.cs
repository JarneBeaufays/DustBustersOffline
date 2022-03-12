using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerParticle : MonoBehaviour
{
    [SerializeField] private GameObject _landingParticle = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Can"))
        {
            Destroy(this.gameObject);
            GameObject particle = Instantiate(_landingParticle, this.transform.position, Quaternion.Euler(90, 0, 0));
            particle.transform.localScale *= 2;
            Destroy(_landingParticle, particle.GetComponent<ParticleSystem>().main.duration);
        }
    }
}

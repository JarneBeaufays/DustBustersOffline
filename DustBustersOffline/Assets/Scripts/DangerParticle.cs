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
            Vector3 pos = this.transform.position;
            pos.y -= 1f;
            GameObject particle = Instantiate(_landingParticle, pos, Quaternion.Euler(90, 0, 0));
            particle.transform.localScale *= 2;
            Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
        }
    }
}

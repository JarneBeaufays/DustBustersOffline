using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<Player>().Charge();
        GetComponent<AudioSource>().Play();
        GetComponentInChildren<ParticleSystem>().Stop();

        Destroy(this.gameObject, 2.5f);
    }
}

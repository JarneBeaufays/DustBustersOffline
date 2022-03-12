using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;

public class VacuumSucker : MonoBehaviour
{
    [SerializeField] private float _pullForce = 10f;

    List<Rigidbody> _rbToPull = new List<Rigidbody>();

    private void Update()
    {
        if (_rbToPull.Count == 0 || !PhotonNetwork.IsMasterClient) return;

        List<Rigidbody> rbToRemove = new List<Rigidbody>();
        foreach(Rigidbody rb in _rbToPull) 
        {
            if (rb) rb.AddForce((transform.position - rb.transform.position).normalized * _pullForce * Time.deltaTime);
            else rbToRemove.Add(rb);
        }

        foreach(Rigidbody rb in rbToRemove) 
        {
            _rbToPull.Remove(rb);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Add rb for ragdoll if the user doesn't already have one
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (!rb) rb = other.gameObject.AddComponent<Rigidbody>();

            rb.useGravity = false;

            other.GetComponent<Player>().IsBeingVacuumed = true;

            _rbToPull.Add(rb);

            // Disable charcontr during ragdoll
            other.GetComponent<CharacterController>().enabled = false;
        }
        else if (other.CompareTag("Can"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            other.gameObject.GetComponent<Can>()._beingSucked = true;
            rb.useGravity = false;
            rb.drag = 0;
            rb.mass = 1f;
            rb.angularDrag = 0;
            _rbToPull.Add(rb);
        }    
        else if (other.CompareTag("Huisstofmijt"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            other.GetComponent<NavMeshAgent>().enabled = true;
            rb.mass = 1f;
            _rbToPull.Add(rb);
        }
        else if (other.CompareTag("Dust"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            _rbToPull.Add(rb);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_rbToPull.Count == 0) return;

        foreach(Rigidbody rb in _rbToPull) 
        {
            if(rb.gameObject == other.gameObject) 
            {
                // Remove rb from the list
                _rbToPull.Remove(rb);

                if (other.CompareTag("Player"))
                {
                    Destroy(rb);
                    other.GetComponent<CharacterController>().enabled = true;
                    other.transform.rotation = Quaternion.identity;
                    other.GetComponent<Player>().IsBeingVacuumed = false;
                }
                else if (other.CompareTag("Can"))
                {
                    other.gameObject.GetComponent<Can>()._beingSucked = false;
                    rb.useGravity = true;
                    rb.drag = 1000;
                    rb.mass = 1000;
                    rb.angularDrag = 1000;
                }
                else if (other.CompareTag("Huisstofmijt"))
                {
                    other.GetComponent<NavMeshAgent>().enabled = true;
                }
                else if (other.CompareTag("Dust"))
                {
                    rb.velocity = Vector3.zero;
                }

                return;
            }
        }
    }
}

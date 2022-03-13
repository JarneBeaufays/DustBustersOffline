using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _uiText = null;

    public void WinnerWinnerChickenDinner(int playerid, Color playerColor)
    {
        // Disable players
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            player.GetComponent<Player>().enabled = false;
        }

        //Disable huisstofmijten
        GameObject[] huisstofmijten = GameObject.FindGameObjectsWithTag("Huisstofmijt");
        foreach (GameObject mijt in huisstofmijten)
        {
            mijt.GetComponent<Huisstofmijt>().enabled = false;
        }

        //Disable dustSpawner
        DustSpawner dustSpawner = GameObject.FindObjectOfType<DustSpawner>();
        dustSpawner.enabled = false;

        //Disable CanManager
        CanManager canManager = GameObject.FindObjectOfType<CanManager>();
        canManager.enabled = false;

        //Disable LightningSpawner
        LightningSpawner lightningSpawner = GameObject.FindObjectOfType<LightningSpawner>();
        lightningSpawner.enabled = false;

        //Play Sound
        GetComponent<AudioSource>().Play();

        //Spawn Particles
        ParticleSystem[] pss = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in pss)
        {
            ParticleSystem.MainModule m = ps.main;
            m.startColor = playerColor;
            ps.Play();
        }

        //UI Text
        _uiText.text = "Player " + playerid.ToString() + " won!";
        _uiText.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void WinnerWinnerChickenDinner()
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
    }
}

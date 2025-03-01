using TMPro;
using UnityEngine;
using System.Collections;

public class PickupSwitchPart : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI enemyPromt;
    public LayerMask whatIsPlayer;
    private bool playerInRange = false;
    public SwitchPartsCollector switchPartsCollector;
    public GameObject monsterSpawn;
    public GameObject pickUpPartSound;

    private AudioSource audioSource;

    void Start()
    {  
        monsterSpawn.SetActive(false);
        audioSource = pickUpPartSound.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        // Show text when player is in range of part
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {
            playerInRange = true;
            pickupText.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            //get part,play sound and activate monster
            switchPartsCollector.CollectSwitchPart();
            pickupText.gameObject.SetActive(false);
            monsterSpawn.SetActive(true);
            enemyPromt.gameObject.SetActive(true);
            audioSource.Play();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
            // This if statement is using bitwise operators to check a gameobject's layer compared to the wanted layer (WhatIsPlayer)
        if ((whatIsPlayer.value & (1 << other.gameObject.layer)) > 0)
        {

            //remove text when player moves out of range of part
            playerInRange = false;
            pickupText.gameObject.SetActive(false);
        }
    }
}
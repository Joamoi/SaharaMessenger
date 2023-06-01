using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbits : MonoBehaviour
{
    private bool canTalk;

    // Update is called once per frame
    void Update()
    {
        if (canTalk)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canTalk = false;
                PlayerManager.playerInstance.talkText.SetActive(false);
                EventManager.eventInstance.StartCoroutine("Bunnies");
                gameObject.GetComponent<Rabbits>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canTalk = true;
            PlayerManager.playerInstance.talkText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canTalk = false;
            PlayerManager.playerInstance.talkText.SetActive(false);
        }
    }
}
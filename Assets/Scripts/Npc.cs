using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private bool canTalk;
    public string[] speechLines;

    // Update is called once per frame
    void Update()
    {
        if (canTalk)
        {
            if (Input.GetKey(KeyCode.E))
            {
                EventManager.eventInstance.StartCoroutine("Talk", speechLines);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    private bool canTalk;
    public GameObject fox;

    // Update is called once per frame
    void Update()
    {
        if (canTalk)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canTalk = false;
                PlayerManager.playerInstance.talkText.SetActive(false);
                EventManager.eventInstance.StartCoroutine("Rabbit1");

                Vector3 direction = (fox.transform.position - transform.position).normalized;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                gameObject.GetComponent<Rabbit>().enabled = false;
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

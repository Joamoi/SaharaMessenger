using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    private bool canEat;
    public GameObject eggs;

    // Update is called once per frame
    void Update()
    {
        if (canEat)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.playerInstance.StartCoroutine("Eat");
                StartCoroutine("DestroyFood");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canEat = true;
            PlayerManager.playerInstance.eatText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canEat = false;
            PlayerManager.playerInstance.eatText.SetActive(false);
        }
    }

    IEnumerator DestroyFood()
    {
        yield return new WaitForSeconds(1f);
        eggs.SetActive(false);
        gameObject.GetComponent<Eggs>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private bool canEat;

    // Update is called once per frame
    void Update()
    {
        if (canEat)
        {
            if (Input.GetKey(KeyCode.E))
            {
                PlayerManager.playerInstance.Eat();
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
        Destroy(gameObject);
    }
}
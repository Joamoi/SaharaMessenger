using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
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
                Destroy(gameObject);
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
}

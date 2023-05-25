using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : MonoBehaviour
{
    private bool canDrink;

    // Update is called once per frame
    void Update()
    {
        if (canDrink)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerManager.playerInstance.Drink();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canDrink = true;
            PlayerManager.playerInstance.drinkText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canDrink = false;
            PlayerManager.playerInstance.drinkText.SetActive(false);
        }
    }
}

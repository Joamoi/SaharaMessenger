using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventManager : MonoBehaviour
{
    public GameObject fox;
    public Collider startRockCollider;
    public Transform startPos;
    public CinemachineFreeLook cineCam;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartCutScene");
    }

    IEnumerator StartCutScene()
    {
        PlayerManager.playerInstance.canMove = false;


        startRockCollider.enabled = false;
        fox.transform.position = startPos.position;

        yield return new WaitForSeconds(2f);

        PlayerManager.playerInstance.x = -1f;
        PlayerManager.playerInstance.z = -1f;

        yield return new WaitForSeconds(3f);

        PlayerManager.playerInstance.x = 0f;
        PlayerManager.playerInstance.z = 0f;

        PlayerManager.playerInstance.canMove = true;
        startRockCollider.enabled = true;
    }
}

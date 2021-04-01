using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public KeyCode key = KeyCode.V;
    private Transform player;
    public GameObject dialogue;
    private bool Isspeak = false;
    public TeleportPoint sceneOne, SceneTwo;

    private void Start()
    {
        player = FindObjectOfType<ChaWitch>().transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < 2)
        {
            if (Input.GetKeyDown(key))
            {
                Isspeak = !Isspeak;
                dialogue.SetActive(Isspeak);

                if (Isspeak == true)
                {

                    //player.GetComponent<PlayerController>().enabled = true;
                    //sceneOne.openTeleport = true;
                    //SceneTwo.openTeleport = true;
                    ChaWitch pc = player.GetComponent<ChaWitch>();
                    if (pc.dragonSkingCount == 0)
                        pc.dragonSkingCount++;
                }
                else
                {
                    enabled = false;
                  //  player.GetComponent<ChaWitch>().enabled = false;
                }
            }
        }
    }
}

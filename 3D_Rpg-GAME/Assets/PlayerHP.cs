using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider sliderhp;
    public ChaWitch player;

    private void Start()
    {
        sliderhp.maxValue = player.Hp;
    }

    private void Update()
    {
        sliderhp.value = player.Hp;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enermybloodbar : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);//血量固定對著攝影機(掛載在血條Canvas)
    }

}

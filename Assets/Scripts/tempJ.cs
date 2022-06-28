using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempJ : MonoBehaviour
{

    public Lane LANE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            LANE.LaneButtonPressed(2);
            Debug.Log("JClick");
        }
    }
}

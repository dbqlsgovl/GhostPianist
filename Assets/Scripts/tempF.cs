using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempF : MonoBehaviour
{
    // Start is called before the first frame update
    public Lane LANE;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LANE.LaneButtonPressed(1);
            Debug.Log("FClick");
        }
    }
}

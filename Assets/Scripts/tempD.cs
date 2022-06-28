using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempD : MonoBehaviour
{
    public Lane LANE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            LANE.LaneButtonPressed(0);
            //Debug.Log("DClick");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempK : MonoBehaviour
{
    public Lane LANE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LANE.LaneButtonPressed(3);
            Debug.Log("KClick");
        }
    }
}

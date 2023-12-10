using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyListener : MonoBehaviour
{
    public int buttonRead;
    public float potentiometerRead;

    void OnMessageArrived(string msg)
    {
        //Debug.Log("Arrived: " + msg);
        string[] split = msg.Split(",");
        potentiometerRead = float.Parse(split[0]);
        buttonRead = int.Parse(split[1]);
        Debug.Log(potentiometerRead + " " + buttonRead);
    }
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}

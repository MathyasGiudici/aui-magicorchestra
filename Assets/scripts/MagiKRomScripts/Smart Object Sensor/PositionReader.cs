﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReader : MonoBehaviour {
    /// <summary>
    /// is the sensor active?
    /// </summary>
    public bool sensorEnabled;
    /// <summary>
    /// the gyroscope of the object
    /// </summary>
    public Vector3[] gyroscope;
    public float[] pitch = new float[1], roll = new float[1];
    public Dictionary<string, int> gyroscopeNames;
    /// <summary>
    /// the accelerometer fo the object
    /// </summary>
    public Vector3[] accelerometer;
    public Dictionary<string, int> accelerometerNames;
    /// <summary>
    /// the position of the object
    /// </summary>
    public Vector3 position;

    /// <summary>
    /// update the state
    /// </summary>
    /// <param name="positionstate"></param>
    internal void updateState(PositionSerializedState positionstate)
    {
        sensorEnabled = positionstate.isEnabled;
        gyroscope[gyroscopeNames[positionstate.namsesensorGyroscope]] = new Vector3(positionstate.gyroscope[0], positionstate.gyroscope[1], positionstate.gyroscope[2]);
        accelerometer[accelerometerNames[positionstate.namsesensorAccelerometer]] = new Vector3(positionstate.accelerometer[0], positionstate.accelerometer[1], positionstate.accelerometer[2]);
        position = new Vector3(positionstate.position[0], positionstate.position[1], positionstate.position[2]);
    }

    internal void setAccelerometer(sensorstreamposition[] sensarr)
    {
        foreach (sensorstreamposition sens in sensarr) {
            this.accelerometer[accelerometerNames[sens.sensorId]] = new Vector3(sens.x, sens.y, sens.z);
        }
    }

    internal void setGyroscope(sensorstreamposition[] sensarr)
    {
        foreach (sensorstreamposition sens in sensarr)
        {
            this.gyroscope[gyroscopeNames[sens.sensorId]] = new Vector3(sens.x, sens.y, sens.z);
        }
    }

    internal void setPosition(float[] position)
    {
        this.position = new Vector3(position[0], position[1], position[2]); ;
    }

    internal void updatePosFromUDP(string val, string act, int dur)
    {
        val = val.Substring(1, val.Length - 2);
        string[] v = val.Split(',');
        setPosition(new float[] { float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]) });
    }
    public void setAccelerometers(string[] names) {
        accelerometer = new Vector3[names.Length];
        roll = new float[names.Length];
        pitch = new float[names.Length];
        accelerometerNames = new Dictionary<string, int>();
        int i = 0;
        foreach (string s in names) {
            accelerometerNames.Add(s, i);
            i++;
        }
        
    }
    public void setGyroscopes(string[] names)
    {
        gyroscope = new Vector3[names.Length];
        gyroscopeNames = new Dictionary<string, int>();
        int i = 0;
        foreach (string s in names)
        {
            gyroscopeNames.Add(s, i);
            i++;
        }
    }

    private void Update()
    {
        for (int i = 0; i < accelerometer.Length; i++)
        { 

            float accelerationX = accelerometer[i].x * 3.9f;
            float accelerationY = accelerometer[i].y * 3.9f;
            float accelerationZ = accelerometer[i].z * 3.9f;
            roll[i] = -(Mathf.Atan2(accelerationX, Mathf.Sqrt(accelerationY*accelerationY + accelerationZ*accelerationZ))*180.0f)/ Mathf.PI;
            pitch[i] = (Mathf.Atan2(accelerationY, accelerationZ) * 180.0f) / Mathf.PI;
        }
    }
}

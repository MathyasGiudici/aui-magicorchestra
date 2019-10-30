using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MagicRoomOlfactoryMachineManager : MonoBehaviour {

    /// <summary>
    /// Singleton of the Script
    /// </summary>
    public static MagicRoomOlfactoryMachineManager instance;
    /// <summary>
    /// True if the smart applianced middleware is open, false otherwise
    /// </summary>
    public bool MagicRoomAppliancesManager_active;

    /// <summary>
    /// list of the smart apliances found by the middleware
    /// </summary>
    public string[] listofAssociatedNames;

    /// <summary>
    /// http address of the middleware
    /// </summary>
    private string address;
    /// <summary>
    /// the command to be sent to the middleware
    /// </summary>    
    private OlfactoryMachineCommand command;

    // Use this for initialization
    void Awake () {
        instance = this;
        address = "http://localhost:7078";
        command = new OlfactoryMachineCommand();
        command.type = "OlfactoryCommand";
        MagicRoomAppliancesManager_active = true;
    }

    private void Start()
    {
        Logger.addToLogNewLine("ServerOM", "Searched Magic Room Olfactory Machines");
        StartCoroutine(sendConfigurationRequest());
    }

    /// <summary>
    /// Encodes the request to import the list of associated names
    /// </summary>
    /// <returns></returns>
    IEnumerator sendConfigurationRequest()
    {
        SmartPlugCommand cmd = new SmartPlugCommand();
        cmd.type = "OlfacotryDiscovery";
        string json = JsonUtility.ToJson(cmd);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest www = UnityWebRequest.Put(address, myData);
        yield return www.Send();
        if (www.isNetworkError)
        {
            if (www.error == "Cannot connect to destination host")
            {
                MagicRoomAppliancesManager_active = false;
            }
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ServerOlfatorymachineConfiguration conf = new ServerOlfatorymachineConfiguration();
            conf = JsonUtility.FromJson<ServerOlfatorymachineConfiguration>(www.downloadHandler.text);
            listofAssociatedNames = conf.configuration;
            string log = "";
            foreach (string s in listofAssociatedNames)
            {
                log += "Found " + s + " on the network, ";
            }
            Logger.addToLogNewLine("ServerOM", log);
            
        }
    }

    /// <summary>
    /// Send the command to change the state of a smart appliance
    /// </summary>
    /// <param name="appliance"> the name of the appliance you want to switch from the list of associated names</param>
    /// <param name="duration">duration in millisencons of the spray</param>
    public void sendStartSmellCommand(string appliance, int duration)
    {
        if (!MagicRoomAppliancesManager_active)
        {
            return;
        }
        command.type = "OlfactoryCommand";
        command.command = "ON";
        command.duration = Mathf.Abs(duration);
        command.id = appliance;
        Logger.addToLogNewLine(appliance, duration.ToString());
        StartCoroutine(sendCommand());
    }

    /// <summary>
    /// send the http crequest to the smart appliance
    /// </summary>
    /// <returns></returns>
    IEnumerator sendCommand()
    {
        string json = JsonUtility.ToJson(command);
        print(json);
        byte[] myData = System.Text.Encoding.UTF8.GetBytes(json);
        UnityWebRequest www = UnityWebRequest.Put(address, myData);
        yield return www.Send();
        if (www.isNetworkError)
        {
            if (www.error == "Cannot connect to destination host")
            {
                MagicRoomAppliancesManager_active = false;
            }
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }

}
[Serializable]
public class OlfactoryMachineCommand
{
    public string type;
    public string command;
    public string id;
    public int duration = 800;
}
[Serializable]
public class ServerOlfatorymachineConfiguration
{
    public string[] configuration;
}

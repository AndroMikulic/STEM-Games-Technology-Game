using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using UnityEditor;

using System;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class Hearbeat : MonoBehaviour
{
    [SerializeField]
    string Heartbeat_IP;
    [SerializeField]
    int Heartbeat_port;
    [SerializeField]
    int heartBeatTimer;

    public string token;
    [SerializeField]
    Text statusLabel;
    [SerializeField]
    Color[] statusColor;
    [SerializeField]
    string[] statusString = { "Connected", "Disconnected" };
    int status = -1;

    public void StartHeartbeat()
    {
        new Thread(Heartbeater).Start();
    }

    public void Heartbeater()
    {
        bool isPlaying = true;
        Debug.Log("Starting heartbeat...");
        try
        {
            TcpClient hbSocket = new TcpClient(Heartbeat_IP, Heartbeat_port);
            Debug.Log("Connected to Heartbeat socket.");
            NetworkStream stream = hbSocket.GetStream();
            byte[] tokenData = System.Text.Encoding.ASCII.GetBytes(token);
            byte[] response = new byte[16];
            string resposneString = "";
            while (true)
            {
                try
                {
                    stream.Write(tokenData, 0, tokenData.Length);
                    response = new byte[16];
                    Int32 bytes = stream.Read(response, 0, response.Length);
                    resposneString = System.Text.Encoding.ASCII.GetString(response, 0, bytes);
                    if (resposneString.Equals("OK"))
                    {
                        status = 0;
                    }
                    else
                    {
                        status = 1;
                    }
                    Thread.Sleep(heartBeatTimer);
                }
                catch (Exception e)
                {
                    status = 1;
                    break;
                }
            }
        }
        catch (Exception e)
        {

        }
        if(isPlaying)
        {
            StartHeartbeat();
        }
    }

    void Update()
    {
        if (status != -1)
        {
            statusLabel.color = statusColor[status];
            statusLabel.text = statusString[status];
        }
    }
}

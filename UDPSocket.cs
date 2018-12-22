using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSocket : MonoBehaviour
{
    public int receivePort;
    public int sendPort;

    Thread thread;

    UdpClient udpReceive; // 6000
    UdpClient udpSend; // 8888
    static readonly object lockObject = new object();

    string receiveData = "welcome";
    string msg = "hello";

    // Start is called before the first frame update
    void Start()
    {
        udpSend = new UdpClient(sendPort);

        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ---------- receive data ----------
    private void ThreadMethod()
    {
        udpReceive = new UdpClient(receivePort);

        while(true)
        {
            IPEndPoint RemoteIPEndPoint = new IPEndPoint(IPAddress.Any, receivePort);
            print(RemoteIPEndPoint);

            byte[] receiveBytes = udpReceive.Receive(ref RemoteIPEndPoint);

            lock (lockObject)
            {
                receiveData = Encoding.ASCII.GetString(receiveBytes);
                print(receiveData);
            }
        }
    }

    // ---------- receive data ----------
    public void SendSocketData()
    {
        IPEndPoint TargetIPEndPoint = new IPEndPoint(IPAddress.Broadcast, sendPort);
        byte[] sendBytes = Encoding.UTF8.GetBytes(msg);
        udpSend.Send(sendBytes, sendBytes.Length, TargetIPEndPoint);
    }

    // ---------- quit applucation ----------
    private void OnApplicationQuit()
    {
        udpReceive.Close();
        udpSend.Close();
        thread.Abort();
    }
}

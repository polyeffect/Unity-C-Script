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

    // Start is called before the first frame update
    void Start()
    {
        udpSend = new UdpClient(sendPort);

        thread = new Thread(new ThreadStart(ThreadMethod));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ThreadMethod()
    {
        udpReceive = new UdpClient(receivePort);
    }
}

using System;
using System.Net;
using System.Net.Sockets;

namespace Multiplayer
{
    public static partial class NetworkUtilities
    {
        public static IPAddress GetLocalIPv4AddressRequiresInternet()
        {
            var localIp = IPAddress.None;
            try
            {
                using (var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    // Connect socket to Google's Public DNS service
                    socket.Connect("8.8.8.8", 65530);
                    if (!(socket.LocalEndPoint is IPEndPoint endPoint))
                    {
                        throw new InvalidOperationException($"Error occurred casting {socket.LocalEndPoint} to IPEndPoint");
                    }
                    localIp = endPoint.Address;
                }
            }
            catch (SocketException)
            {
                // Handle exception
            }

            return localIp;
        }
    }
}
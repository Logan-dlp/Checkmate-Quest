using System;
using System.Net;
using System.Net.Sockets;

namespace Multiplayer
{
    public static partial class NetworkUtilities
    {
        public static IPAddress GetLocalIPv4Address()
        {
            var localIp = IPAddress.None;
            try
            {
                using (var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    if (!(socket.LocalEndPoint is IPEndPoint endPoint))
                    {
                        throw new InvalidOperationException($"Error occurred casting {socket.LocalEndPoint} to IPEndPoint");
                    }
                    localIp = endPoint.Address;
                }
            }
            catch (SocketException) {}

            return localIp;
        }
    }
}
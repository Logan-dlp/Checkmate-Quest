using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

namespace Display
{
    public class DisplayIPAdress : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _ipAdressText;

        private void Start()
        {
            _ipAdressText.text = GetLocalIPAddress();
        }

        public string GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddress.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
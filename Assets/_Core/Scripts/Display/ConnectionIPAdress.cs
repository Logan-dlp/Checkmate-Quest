using System;
using System.Net;
using Multiplayer;
using TMPro;
using UnityEngine;

namespace Display
{
    public class ConnectionIPAdress : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _ipV4AdressText;

        private void OnEnable()
        {
            DisplayIPV4Adress();
        }

        private void DisplayIPV4Adress()
        {
            IPAddress ipAddress = NetworkUtilities.GetLocalIPv4AddressRequiresInternet();
            _ipV4AdressText.text = ipAddress.ToString();
        }
    }
}
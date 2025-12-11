using System.Collections;
using UnityEngine;

namespace App.Weather
{
    public class LocationServiceManager : MonoBehaviour
    {
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }
        public bool IsReady { get; private set; }
        public string Error { get; private set; }

        private IEnumerator Start()
        {
            if (!Input.location.isEnabledByUser)
            {
                Error = "Location services not enabled by user.";
                yield break;
            }

            Input.location.Start();

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait <= 0)
            {
                Error = "Location service timed out.";
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Error = "Unable to determine device location.";
                yield break;
            }

            Latitude = Input.location.lastData.latitude;
            Longitude = Input.location.lastData.longitude;
            IsReady = true;
        }
    }
}

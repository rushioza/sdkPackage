using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace App.Weather
{
    public class WeatherService
    {
        [Serializable]
        private class WeatherResponse
        {
            public CurrentWeather current_weather;
        }

        [Serializable]
        private class CurrentWeather
        {
            public float temperature;
        }

        public IEnumerator GetCurrentTemperature(
            float latitude,
            float longitude,
            Action<float> onSuccess,
            Action<string> onError
        )
        {
            string url =
                $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";

            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
                if (www.result != UnityWebRequest.Result.Success)
#else
                if (www.isNetworkError || www.isHttpError)
#endif
                {
                    onError?.Invoke(www.error);
                    yield break;
                }

                try
                {
                    string json = www.downloadHandler.text;
                    var response = JsonUtility.FromJson<WeatherResponse>(json);

                    if (response == null || response.current_weather == null)
                    {
                        onError?.Invoke("Invalid weather response");
                        yield break;
                    }

                    onSuccess?.Invoke(response.current_weather.temperature);
                }
                catch (Exception e)
                {
                    onError?.Invoke(e.Message);
                }
            }
        }
    }
}

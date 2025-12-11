using System.Collections;
using App.Weather;
using CleverTap.UnitySDK;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Weather
{
    public class WeatherUIController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private LocationServiceManager locationServiceManager;

        [SerializeField]
        private Button fetchButton;

        [SerializeField]
        private TextMeshProUGUI latitudeText;

        [SerializeField]
        private TextMeshProUGUI longitudeText;

        [SerializeField]
        private TextMeshProUGUI temperatureText;

        [SerializeField]
        private TextMeshProUGUI statusText;

        private WeatherService _weatherService;

        private void Awake()
        {
            _weatherService = new WeatherService();
            fetchButton.onClick.AddListener(OnFetchClicked);
        }

        private void Update()
        {
            if (locationServiceManager == null)
                return;

            if (locationServiceManager.IsReady)
            {
                latitudeText.text = $"Lat: {locationServiceManager.Latitude:F4}";
                longitudeText.text = $"Lon: {locationServiceManager.Longitude:F4}";
            }
            else if (!string.IsNullOrEmpty(locationServiceManager.Error))
            {
                statusText.text = locationServiceManager.Error;
            }
        }

        private void OnFetchClicked()
        {
            if (!locationServiceManager.IsReady)
            {
                statusText.text = "Location not ready yet.";
                return;
            }

            statusText.text = "Fetching weather...";

            StartCoroutine(
                _weatherService.GetCurrentTemperature(
                    locationServiceManager.Latitude,
                    locationServiceManager.Longitude,
                    OnWeatherSuccess,
                    OnWeatherError
                )
            );
        }

        private void OnWeatherSuccess(float temperature)
        {
            temperatureText.text = $"Temp: {temperature:F1} °C";
            statusText.text = "Weather fetched.";

            ToastService.Show($"Current temperature: {temperature:F1} °C");
        }

        private void OnWeatherError(string error)
        {
            statusText.text = $"Error: {error}";
            ToastService.Show("Failed to fetch weather.");
        }
    }
}

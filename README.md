# sdkPackage
CleverTap Unity SDK – Toast + Weather Demo

This project contains a Unity SDK Package that provides a reusable Toast API for Android & iOS, along with a sample Weather App demonstrating its usage.

Project Overview : 

SDK Features

ToastService.Show(message) — single cross-platform API.

ToastButton.prefab — reusable GameObject that triggers Toast on click.

Native platform implementations included:

Android: Java plugin (ToastPlugin.java)

iOS: Objective-C plugin (CleverTapToast.mm)

Weather App Features

Fetches Latitude & Longitude using Unity Location Service.

Calls Open-Meteo API to get the current temperature.

Displays temperature on UI.

Shows weather details using SDK ToastService.

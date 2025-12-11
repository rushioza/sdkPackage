using UnityEngine;

namespace CleverTap.UnitySDK
{
    public static class ToastService
    {
        public static void Show(string message)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            ShowAndroid(message);
#elif UNITY_IOS && !UNITY_EDITOR
            ShowIOS(message);
#else
            Debug.Log("[ToastService] " + message);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private static void ShowAndroid(string message)
        {
            using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                using (var pluginClass = new AndroidJavaClass("com.clevertap.sdk.ToastPlugin"))
                {
                    pluginClass.CallStatic("showToast", activity, message);
                }
            }
        }
#endif

#if UNITY_IOS && !UNITY_EDITOR
        [System.Runtime.InteropServices.DllImport("__Internal")]
        private static extern void ct_show_toast(string message);

        private static void ShowIOS(string message)
        {
            ct_show_toast(message);
        }
#endif
    }
}

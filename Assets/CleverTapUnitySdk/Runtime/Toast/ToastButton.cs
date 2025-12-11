using UnityEngine;
using UnityEngine.UI;

namespace CleverTap.UnitySDK
{
    [RequireComponent(typeof(Button))]
    public class ToastButton : MonoBehaviour
    {
        [SerializeField]
        private string toastMessage = "Hello from CleverTap Unity SDK Toast!";

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ToastService.Show(toastMessage);
        }
    }
}

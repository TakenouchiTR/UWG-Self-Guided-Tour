using System.Collections;
using UnityEngine;


public class LocationServiceTest : MonoBehaviour
{
    #region Methods

    private IEnumerator LocationCoroutine()
    {
        // Uncomment if you want to test with Unity Remote
        /*#if UNITY_EDITOR
                yield return new WaitWhile(() => !UnityEditor.EditorApplication.isRemoteConnected);
                yield return new WaitForSecondsRealtime(5f);
        #endif*/
#if UNITY_EDITOR
        // No permission handling needed in Editor
#elif UNITY_ANDROID
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation)) {
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);
        }

        // First, check if user has location service enabled
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("Android and Location not enabled");
            yield break;
        }

#elif UNITY_IOS
        if (!UnityEngine.Input.location.isEnabledByUser) {
            // TODO Failure
            Debug.LogFormat("IOS and Location not enabled");
            yield break;
        }
#endif
        // Start service before querying location
        Input.location.Start(500f, 500f);

        // Wait until service initializes
        var maxWait = 15;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            maxWait--;
        }

        // Editor has a bug which doesn't set the service status to Initializing. So extra wait in Editor.
#if UNITY_EDITOR
        var editorMaxWait = 15;
        while (Input.location.status == LocationServiceStatus.Stopped && editorMaxWait > 0)
        {
            yield return new WaitForSecondsRealtime(1);
            editorMaxWait--;
        }
#endif

        // Service didn't initialize in 15 seconds
        if (maxWait < 1)
        {
            // TODO Failure
            Debug.LogFormat("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status != LocationServiceStatus.Running)
        {
            // TODO Failure
            Debug.LogFormat("Unable to determine device location. Failed with status {0}", Input.location.status);
            yield break;
        }

        Debug.LogFormat("Location service live. status {0}", Input.location.status);
        // Access granted and location value could be retrieved
        Debug.LogFormat("Location: "
                        + Input.location.lastData.latitude + " "
                        + Input.location.lastData.longitude + " "
                        + Input.location.lastData.altitude + " "
                        + Input.location.lastData.horizontalAccuracy + " "
                        + Input.location.lastData.timestamp);

        var _latitude = Input.location.lastData.latitude;
        var _longitude = Input.location.lastData.longitude;
        // TODO success do something with location

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();
    }

    #endregion
}
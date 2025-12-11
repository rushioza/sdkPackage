package com.clevertap.sdk;

import android.app.Activity;
import android.widget.Toast;

public class ToastPlugin {

    public static void showToast(final Activity activity, final String message) {
        if (activity == null) return;

        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(activity.getApplicationContext(), message, Toast.LENGTH_SHORT).show();
            }
        });
    }
}

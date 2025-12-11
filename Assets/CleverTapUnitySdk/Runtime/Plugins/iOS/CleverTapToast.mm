#import <UIKit/UIKit.h>

extern "C" void ct_show_toast(const char *messageC)
{
    if (messageC == NULL) return;

    NSString *message = [NSString stringWithUTF8String:messageC];

    dispatch_async(dispatch_get_main_queue(), ^{
        UIWindow *keyWindow = [UIApplication sharedApplication].keyWindow;
        if (!keyWindow) {
            return;
        }

        UIViewController *rootViewController = keyWindow.rootViewController;
        if (!rootViewController) {
            return;
        }

        // Simple alert style "toast" for demo
        UIAlertController *alert = [UIAlertController alertControllerWithTitle:nil
                                                                       message:message
                                                                preferredStyle:UIAlertControllerStyleAlert];

        [rootViewController presentViewController:alert animated:YES completion:nil];

        // Dismiss automatically after 2 seconds
        dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(2.0 * NSEC_PER_SEC)),
                       dispatch_get_main_queue(), ^{
            [alert dismissViewControllerAnimated:YES completion:nil];
        });
    });
}

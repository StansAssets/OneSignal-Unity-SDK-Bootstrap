using System.Collections.Generic;

namespace Com.OneSignal.Bootstrapper
{
    static class BootstrapperConfig
    {
        public static readonly string OneSignalScope = "com.onesignal-test";
        public static readonly string BootstrapperPackageName = $"{OneSignalScope}.unity.bootstrap";

        public static readonly string OneSignalCoreName = $"{OneSignalScope}.unity.core";
        public static readonly string OneSignalCoreVersion = "0.0.4-preview";

        public static readonly string OneSignalIOSName = $"{OneSignalScope}.unity.ios";
        public static readonly string OneSignalIosVersion = "0.0.4-preview";

        public static readonly string OneSignalAndroidName = $"{OneSignalScope}.unity.android";
        public static readonly string OneSignalAndroidVersion = "0.0.4-preview";

        public static readonly string GoogleScopeRegistryUrl = "https://unityregistry-pa.googleapis.com";
        public static readonly string NpmjsScopeRegistryUrl = "https://registry.npmjs.org/";

        public static ScopeRegistry GoogleScopeRegistry =>
            new ScopeRegistry("Game Package Registry by Google",
                GoogleScopeRegistryUrl,
                new HashSet<string>
                {
                    "com.google"
                });

        public static ScopeRegistry NpmjsScopeRegistry =>
            new ScopeRegistry("npmjs",
                NpmjsScopeRegistryUrl,
                new HashSet<string>
                {
                    OneSignalScope
                });
    }
}

using UnityEditor;

namespace Com.OneSignal.Bootstrapper
{
    [InitializeOnLoad]
    static class Bootstrapper
    {
        static Bootstrapper()
        {
            var manifest = new Manifest();
            manifest.Fetch();

            var manifestUpdated = false;

            if (!manifest.IsRegistryExists(BootstrapperConfig.NpmjsScopeRegistryUrl))
            {
                manifest.AddScopeRegistry(BootstrapperConfig.NpmjsScopeRegistry);
                manifestUpdated = true;
            }
            else
            {
                var npmjsScopeRegistry = manifest.GetScopeRegistry(BootstrapperConfig.NpmjsScopeRegistryUrl);
                if (!npmjsScopeRegistry.HasScope(BootstrapperConfig.OneSignalScope))
                {
                    npmjsScopeRegistry.AddScope(BootstrapperConfig.OneSignalScope);
                    manifestUpdated = true;
                }
            }

            // UnityEditor.PackageManager.Client.Add method doesn't work in Unity versions older then 2019.
            // Thus, we need to manually add dependencies.
            // Probably we need to make something similar to OneSignalUpdateRequest to get the latest package version.

            if (!manifest.IsDependencyExists(BootstrapperConfig.OneSignalAndroidName))
            {
                manifest.AddDependency(BootstrapperConfig.OneSignalAndroidName, BootstrapperConfig.OneSignalAndroidVersion);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(BootstrapperConfig.OneSignalIOSName))
            {
                manifest.AddDependency(BootstrapperConfig.OneSignalIOSName, BootstrapperConfig.OneSignalIosVersion);
                manifestUpdated = true;
            }

            if (manifestUpdated)
            {
                manifest.ApplyChanges();
            }

            UnityEditor.PackageManager.Client.Remove(BootstrapperConfig.BootstrapperPackageName);
        }
    }
}

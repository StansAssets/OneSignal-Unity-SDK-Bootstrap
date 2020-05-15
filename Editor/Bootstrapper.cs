using UnityEditor;

namespace OneSignalPush.Bootstrapper
{
    [InitializeOnLoad]
    static class Bootstrapper
    {
        static Bootstrapper()
        {
            Manifest manifest = new Manifest();
            manifest.Fetch();

            bool manifestUpdated = false;

            if (!manifest.IsRegistryExists(ScopeRegistriesConfig.NpmjsScopeRegistryUrl))
            {
                manifest.AddScopeRegistry(ScopeRegistriesConfig.NpmjsScopeRegistry);
                manifestUpdated = true;
            }
            else
            {
                var npmjsScopeRegistry = manifest.GetScopeRegistry(ScopeRegistriesConfig.NpmjsScopeRegistryUrl);
                if (!npmjsScopeRegistry.HasScope(ScopeRegistriesConfig.OneSignalScope))
                {
                    npmjsScopeRegistry.AddScope(ScopeRegistriesConfig.OneSignalScope);
                    manifestUpdated = true;
                }
            }

            // UnityEditor.PackageManager.Client.Add(ScopeRegistriesConfig.OneSignalCoreName);
            // UnityEditor.PackageManager.Client.Add method doesn't work in Unity versions older then 2019.
            // Thus, we need to manually add dependencies.
            // Probably we need to use OneSignalUpdateRequest to get the latest package version

            if (!manifest.IsDependencyExists(ScopeRegistriesConfig.OneSignalCoreName))
            {
                manifest.AddDependency(ScopeRegistriesConfig.OneSignalCoreName, ScopeRegistriesConfig.OneSignalCoreVersion);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(ScopeRegistriesConfig.OneSignalAndroidName))
            {
                manifest.AddDependency(ScopeRegistriesConfig.OneSignalAndroidName, ScopeRegistriesConfig.OneSignalAndroidVersion);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(ScopeRegistriesConfig.OneSignaliOSName))
            {
                manifest.AddDependency(ScopeRegistriesConfig.OneSignaliOSName, ScopeRegistriesConfig.OneSignalIosVersion);
                manifestUpdated = true;
            }

            if (manifestUpdated)
            {
                manifest.ApplyChanges();
            }
        }
    }
}

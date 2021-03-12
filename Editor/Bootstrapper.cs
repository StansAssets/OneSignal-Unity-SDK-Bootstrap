using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Com.OneSignal.Bootstrapper
{
    [InitializeOnLoad]
    static class Bootstrapper
    {
        static Bootstrapper ()
        {
            if (!AssetDatabase.FindAssets ("lock", new[] { BootstrapperConfig.BootstrapperFolderPath }).Any ()) {
                AssetDatabase.Refresh();
                InstallLatestOneSignalRelease (true);
            }
            else {
                Debug.Log ("'lock' file found. Bootstrap execution has not started.");
            }
        }

        internal static void InstallLatestOneSignalRelease (bool cleanUp)
        {
            GitHubUtility.GetLatestRelease (BootstrapperConfig.GitHubRepositoryURL, Bootstrap);
            if (cleanUp) {
                UnityEditor.PackageManager.Client.Remove(BootstrapperConfig.BootstrapperPackageName);
                CleanUpUtility.RemoveBootstrapperAssets ();
            }
        }
        
        static void Bootstrap(GitHubRelease latestRelease)
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
                manifest.AddDependency(BootstrapperConfig.OneSignalAndroidName, latestRelease.Name);
                manifestUpdated = true;
            }

            if (!manifest.IsDependencyExists(BootstrapperConfig.OneSignalIOSName))
            {
                manifest.AddDependency(BootstrapperConfig.OneSignalIOSName, latestRelease.Name);
                manifestUpdated = true;
            }

            if (manifestUpdated)
            {
                manifest.ApplyChanges();
            }
        }
    }
}

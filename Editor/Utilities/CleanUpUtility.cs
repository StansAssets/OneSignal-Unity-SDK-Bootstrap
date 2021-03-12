using UnityEditor;

namespace Com.OneSignal.Bootstrapper
{
    static class CleanUpUtility
    {
        internal static void RemoveBootstrapperAssets ()
        {
            if (AssetDatabase.IsValidFolder (BootstrapperConfig.BootstrapperFolderPath))
            {
#if UNITY_2020
                var failedPathsList = new List<string>();
                if (!AssetDatabase.DeleteAssets (new[]{BootstrapperConfig.BootstrapperFolderPath}, failedPathsList)){
                    failedPathsList.ForEach(Debug.Log);
                }
                else {
                    Debug.Log ($"Folder {RemovalPath} not found!");
                }
#else
                FileUtil.DeleteFileOrDirectory(BootstrapperConfig.BootstrapperFolderPath);
                FileUtil.DeleteFileOrDirectory(BootstrapperConfig.BootstrapperFolderPath + ".meta");
                AssetDatabase.Refresh();
#endif
            }
        }
    }
}

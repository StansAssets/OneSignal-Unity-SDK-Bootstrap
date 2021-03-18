using System.Linq;
using UnityEditor;

namespace Com.OneSignal.Bootstrapper
{
    static class CleanUpUtility
    {
        internal static void RemoveDirectories(params string[] directories)
        {
            var validDirectories = directories.Where(AssetDatabase.IsValidFolder).ToArray();
            if (validDirectories.Any()) {
#if UNITY_2020
                var failedPathsList = new List<string>();
                if (!AssetDatabase.DeleteAssets (validFolders, failedPathsList)){
                    failedPathsList.ForEach(Debug.Log);
                }
                else {
                    Debug.Log ($"Directory {RemovalPath} not found!");
                }
#else
                foreach (var path in validDirectories) {
                    FileUtil.DeleteFileOrDirectory(path);
                    FileUtil.DeleteFileOrDirectory(path + ".meta");
                }

                AssetDatabase.Refresh();
#endif
            }
        }
    }
}

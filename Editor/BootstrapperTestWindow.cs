using UnityEditor;
using UnityEngine.UIElements;

namespace Com.OneSignal.Bootstrapper
{
    public class BootstrapperTestWindow : EditorWindow
    {
        static Label s_LatestVersionLabel;

        [MenuItem("OneSignalBootstrap/TestWindow")]
        static void Init ()
        {
            var window = (BootstrapperTestWindow)EditorWindow.GetWindow (typeof (BootstrapperTestWindow));
            window.Show();   
        }
        
        void OnEnable ()
        {
            rootVisualElement.Clear ();
            rootVisualElement.Add (new Button (CleanUpUtility.RemoveBootstrapperAssets) {text = "Clean Up"});
            rootVisualElement.Add (new Button (() => Bootstrapper.InstallLatestOneSignalRelease ()) {text = "Install OneSignal Packages (no cleanup)"});
            rootVisualElement.Add (new Button (() => Bootstrapper.InstallLatestOneSignalRelease ()) {text = "Install OneSignal Packages (complete)"});
            
            rootVisualElement.Add (s_LatestVersionLabel ?? (s_LatestVersionLabel = new Label ("latest package version is unknown")));
            rootVisualElement.Add (new Button (() => GitHubUtility.GetLatestRelease (BootstrapperConfig.GitHubRepositoryURL,
                                                                                     release => s_LatestVersionLabel.text = $"latest package version is {release.Name}"))
                                   { text = "Load latest package version" });
        }
    }
}

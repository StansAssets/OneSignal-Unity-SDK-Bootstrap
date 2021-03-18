using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Com.OneSignal.Bootstrapper
{
    static class GitHubUtility
    {
        internal static void GetLatestRelease(string url, Action<GitHubRelease> callback)
        {
            var rq = UnityWebRequest.Get(GetReleaseInfoURL(url));
            rq.SendWebRequest().completed += obj => {
                callback(new GitHubRelease(rq.downloadHandler.text));
            };
        }

        static string GetReleaseInfoURL(string repositoryURL)
        {
            if (repositoryURL.Contains("github.com")) {
                return repositoryURL.Replace(@".git", @"/releases/latest")
                                    .Replace(@"ssh://git@github.com:", @"https://api.github.com/repos/");
            }

            throw new InvalidOperationException($"The provided URL {repositoryURL} is not a GitHub repository URL.");
        }
    }
}

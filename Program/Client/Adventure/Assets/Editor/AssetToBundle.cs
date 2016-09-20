using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

class AssetToBundle
{
    static string prefabPath = Application.dataPath + "/Resources/Prefabs/";
    static List<AssetBundleBuild> buildBundlemaps = new List<AssetBundleBuild>();

    [MenuItem("Tools/Build Andorid Resource")]
    public static void BuildToAndroidAssetBundle()
    {
        BuildAssetResource(BuildTarget.Android);
    }
    [MenuItem("Tools/Build Windows Resource")]
    public static void BuildToWindowsAssetBundle()
    {
        BuildAssetResource(BuildTarget.StandaloneWindows);
    }

    private static void BuildAssetResource(BuildTarget target)
    {
        //string dataPath = GameUtil.GetStreamingAssetsPath();
        //if (Directory.Exists(dataPath))
        //{
        //    Directory.Delete(dataPath, true);
        //}
        buildBundlemaps.Clear();
        string streamPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamPath))
        {
            Directory.Delete(streamPath, true);
        }
        Directory.CreateDirectory(streamPath);
        AssetDatabase.Refresh();

        string[] dirs = Directory.GetDirectories(prefabPath, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++)
        {
            string name = dirs[i].Replace(prefabPath, string.Empty);
            AddBuildMap(name, "*.prefab", dirs[i]);
        }
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle |
                              BuildAssetBundleOptions.UncompressedAssetBundle;
        BuildPipeline.BuildAssetBundles(streamPath, buildBundlemaps.ToArray(), options, target);
        AssetDatabase.Refresh();
    }

    static void AddBuildMap(string bundleName, string pattern, string path)
    {
        string[] files = Directory.GetFiles(path, pattern);
        if (files.Length == 0) return;
        string[] assetFiles = new string[files.Length];

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');
            assetFiles[i] = files[i].Substring(files[i].IndexOf("Asset"));
        }
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = bundleName;
        build.assetNames = assetFiles;
        buildBundlemaps.Add(build);
    }
}

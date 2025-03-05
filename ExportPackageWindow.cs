using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

public class ExportPackageWindow : EditorWindow
{
    private List<string> selectedFolders = new List<string>();
    private bool includeDependencies = true;
    private bool useDefaultPath = true;
    private string customPath = "";
    private bool autoUploadToGitHub = false;
    private bool autoUploadToGoogleDrive = false;

    [MenuItem("Export/Export Package Window")]
    public static void ShowWindow()
    {
        GetWindow<ExportPackageWindow>("Export Package");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select Folders to Export:", EditorStyles.boldLabel);

        string[] allFolders = AssetDatabase.GetAllAssetPaths().Where(path => AssetDatabase.IsValidFolder(path)).ToArray();

        foreach (string folder in allFolders)
        {
            bool selected = selectedFolders.Contains(folder);
            bool newSelected = EditorGUILayout.ToggleLeft(folder, selected);
            if (newSelected && !selected)
                selectedFolders.Add(folder);
            else if (!newSelected && selected)
                selectedFolders.Remove(folder);
        }

        includeDependencies = EditorGUILayout.Toggle("Include Dependencies", includeDependencies);
        useDefaultPath = EditorGUILayout.Toggle("Use Default Path (Builds/Packages)", useDefaultPath);

        if (!useDefaultPath)
        {
            GUILayout.Label("Custom Path:");
            customPath = EditorGUILayout.TextField(customPath);
        }

        GUILayout.Space(10);
        GUILayout.Label("Optional Uploads:", EditorStyles.boldLabel);
        autoUploadToGitHub = EditorGUILayout.Toggle("Upload to GitHub", autoUploadToGitHub);
        autoUploadToGoogleDrive = EditorGUILayout.Toggle("Upload to Google Drive", autoUploadToGoogleDrive);

        if (GUILayout.Button("Export Package"))
        {
            ExportPackage();
        }
    }

    private void ExportPackage()
    {
        if (selectedFolders.Count == 0)
        {
            EditorUtility.DisplayDialog("Error", "Please select at least one folder!", "OK");
            return;
        }

        string version = PlayerSettings.bundleVersion;
        string packageName = $"{PlayerSettings.productName}_v{version}.unitypackage";

        string savePath = useDefaultPath ? Path.Combine("Builds/Packages", packageName) : customPath;

        if (!Directory.Exists("Builds/Packages"))
            Directory.CreateDirectory("Builds/Packages");

        try
        {
            AssetDatabase.ExportPackage(selectedFolders.ToArray(), savePath, includeDependencies ? ExportPackageOptions.IncludeDependencies : ExportPackageOptions.None);
            EditorUtility.DisplayDialog("Success", $"Package exported successfully to {savePath}", "OK");

            if (autoUploadToGitHub)
                UploadToGitHub(savePath);
            if (autoUploadToGoogleDrive)
                UploadToGoogleDrive(savePath);
        }
        catch (System.Exception e)
        {
            EditorUtility.DisplayDialog("Error", "Export failed: " + e.Message, "OK");
        }
    }

    private void UploadToGitHub(string filePath)
    {
        string repoPath = "Path/To/Your/GitHub/Repo";
        string commitMessage = $"Auto-exported package {Path.GetFileName(filePath)}";

        Process.Start("cmd.exe", $"/C cd {repoPath} && git add . && git commit -m \"{commitMessage}\" && git push");
        UnityEngine.Debug.Log("Package uploaded to GitHub.");
    }

    private void UploadToGoogleDrive(string filePath)
    {
        
        UnityEngine.Debug.Log("Package uploaded to Google Drive (placeholder).");
    }
}

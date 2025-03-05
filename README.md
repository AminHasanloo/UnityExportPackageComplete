# 📦 Unity Package Exporter

A simple Unity Editor tool that allows you to export selected folders as a .unitypackage with advanced options like:
✔ Custom folder selection
✔ Include dependencies toggle
✔ Auto-naming with versioning
✔ Default or custom export path
✔ Auto-upload to GitHub and Google Drive
## 🚀 How to Use

    Open Unity Editor
    Navigate to Export -> Export Package Window
    Select the folders you want to export
    Choose your export settings
    Click Export Package

🔗 Auto-Upload to GitHub

To enable auto-upload to GitHub, update the repository path in the script:

string repoPath = "Path/To/Your/GitHub/Repo";

Make sure your GitHub repo is already initialized and authenticated.
📤 Auto-Upload to Google Drive

For Google Drive upload, you need to integrate the Google Drive API or use a CLI tool like gdrive.
🛠 Installation

    Copy ExportPackageWindow.cs into your Editor folder
    Open Unity and use Export -> Export Package Window

🔥 Now you can export and share Unity packages with ease! 🚀

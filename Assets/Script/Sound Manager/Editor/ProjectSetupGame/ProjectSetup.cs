namespace As_Star
{
    using UnityEditor;
    using UnityEngine;
    using System.IO;
    using System.Collections.Generic;

    public class ProjectSetup : EditorWindow
    {
        static ProjectSetup win;

        #region properties
        static string FolderName = "MyResources";




        #endregion

        public static void ShowWindow()
        {
            win = GetWindow<ProjectSetup>(typeof(ProjectSetup));
            win.maxSize= new Vector2(270, 160);
            win.minSize= new Vector2(270, 160);
            win.Show();
        }

        private void OnGUI()
        {
            // showPosition = EditorGUILayout.Foldout(showPosition, status);
            // if (showPosition)
            // {

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            FolderName = EditorGUILayout.TextField("Folder Name", FolderName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            if (GUILayout.Button("Generate", GUILayout.Height(70), GUILayout.ExpandWidth(true)))
            {
                ProjectSetupFolder();
            }

            EditorGUILayout.Space(20);
            EditorGUILayout.EndVertical();
        }

        void ProjectSetupFolder()
        {

            if (string.IsNullOrEmpty(FolderName))
            {
                if (EditorUtility.DisplayDialog("Error", "\"Enter Folder Name\"  is Empty", "OK"))
                {
                    return;

                }
            }

            string rootFolder = Application.dataPath;
            if (Directory.Exists($"{rootFolder}/{FolderName}"))
            {
                if (EditorUtility.DisplayDialog("Error", "Folder already exists", "OK"))
                {
                    return;

                }
            }
            DirectoryInfo folderInfo = Directory.CreateDirectory($"{rootFolder}/{FolderName}");


            if (folderInfo.Exists)
            {
                CreateFolder($"{rootFolder}/{FolderName}");

            }

            AssetDatabase.Refresh();

        }

        void CreateFolder(string EPath)
        {
            List<string> folders = new List<string>();

            DirectoryInfo folderInfo = Directory.CreateDirectory($"{EPath}/Cods");
            if (folderInfo.Exists)
            {

                folders.Clear();
                folders.Add("Scripts");
                folders.Add("Editor");
                CreateSubFolder($"{EPath}/Cods", folders);
            }
            folderInfo = Directory.CreateDirectory($"{EPath}/Art");
            if (folderInfo.Exists)
            {

                folders.Clear();
                folders.Add("Animations");
                folders.Add("Prefabs");
                folders.Add("Shader");
                CreateSubFolder($"{EPath}/Art", folders);
            }
            folderInfo = Directory.CreateDirectory($"{EPath}/Resources");
            if (folderInfo.Exists)
            {

                folders.Clear();
                folders.Add("props");
                folders.Add("UI");
                folders.Add("Characters");
                CreateSubFolder($"{EPath}/Resources", folders);
            }
            folderInfo = Directory.CreateDirectory($"{EPath}/Prefabs");
            if (folderInfo.Exists)
            {

                folders.Clear();
                folders.Add("props");
                folders.Add("UI");
                folders.Add("Characters");
                CreateSubFolder($"{EPath}/Prefabs", folders);
            }


        }
        void CreateSubFolder(string EPath, List<string> folders)
        {
            foreach (var item in folders)
            {
                Directory.CreateDirectory($"{EPath}/{item}");
            }

        }


    }
}



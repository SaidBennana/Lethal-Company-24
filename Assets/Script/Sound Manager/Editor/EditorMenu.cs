using As_Star;
using UnityEditor;

public class EditorMenu
{
    [MenuItem("MyTools/ProjectSetup")]
    public static void ProjectSetup_Open()
    {
        ProjectSetup.ShowWindow();

    }
    [MenuItem("MyTools/SoundManager")]
    public static void SoundManager_Open()
    {
        SoundEditor.openWindow();

    }

}

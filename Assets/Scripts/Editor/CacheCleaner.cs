using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

public class CacheCleaner : Editor
{
    [MenuItem("Tools/Clear Cache")]
    public static void ClearCache()
    {
        PlayerPrefs.DeleteAll();
        ClearDiskSavingData();
        Debug.Log("The cache has been cleared.");
    }

    private static void ClearDiskSavingData()
    {
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath);
        Caching.ClearCache();
    }
}

using UnityEngine;

public class SerializationManager : MonoBehaviour
{
    [SerializeField] private Skins skins;

    // Saving paths and keys
    private const string SKINS_STATE_SAVING_PATH = "/skins-state.json";
    private const string SELECTED_SKIN_SAVING_KEY = "selected-skin-id";

    private void Awake()
    {
        Load();
    }

    private void Load()
    {
        skins.Load(Application.persistentDataPath + SKINS_STATE_SAVING_PATH, SELECTED_SKIN_SAVING_KEY);
    }

    private void Save()
    {
        skins.Save(Application.persistentDataPath + SKINS_STATE_SAVING_PATH, SELECTED_SKIN_SAVING_KEY);
    }

    private void OnDisable()
    {
        Save();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}

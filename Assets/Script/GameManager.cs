using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region variables
    public int roomsCountActive = 3;
    public int levelIndex = 1;
    [HideInInspector] public bool is_Play = false;

    // map and levels
    [SerializeField] Transform[] Rooms;
    // Props
    public Transform[] props;


    #endregion


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);


    }


    private void Start()
    {
        roomsCountActive = LoadData<int>(SaveKeys.roomsCountActive, roomsCountActive);

        for (int i = 0; i < roomsCountActive; i++)
        {
            if (i < Rooms.Length)
                Rooms[i].gameObject.SetActive(true);
        }
        levelIndex = LoadData<int>(SaveKeys.LevelIndex, 1);

    }


    /// Checks if a key exists in the save file and returns the value if it does, otherwise returns the default value.
    public T LoadData<T>(string key, T def)
    {
        if (ES3.FileExists("SaveFile.es3"))
        {
            if (ES3.KeyExists(key, "SaveFile.es3"))
            {
                return ES3.Load<T>(key);

            }

        }
        return def;

    }




}

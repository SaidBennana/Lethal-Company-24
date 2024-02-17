using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    #region variables
    [SerializeField] int roomsCountActive = 3;
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
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);


    }


    private void Start()
    {
        for (int i = 0; i < roomsCountActive; i++)
        {
            if (i < Rooms.Length)
                Rooms[i].gameObject.SetActive(true);
        }
    }


    /// Checks if a key exists in the save file and returns the value if it does, otherwise returns the default value.
    public T ChackKey<T>(string key)
    {
        if (ES3.FileExists("SaveFile.es3"))
        {
            if (ES3.KeyExists(key, "SaveFile.es3"))
            {
                return ES3.Load<T>(key);

            }

        }
        return default;

    }




}

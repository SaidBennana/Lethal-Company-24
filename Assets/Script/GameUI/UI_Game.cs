using System.Collections;
using System.Collections.Generic;
using System.Linq;
using As_Star;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI_Game : MonoBehaviour
{
    public static UI_Game Instance;
    [SerializeField] UIDocument uIDocument;

    // Player
    [SerializeField] GameObject ControllePlayer;


    #region varibalsUI
    VisualElement root;
    private Button Start_GameBtn;
    private Label TextCountaty;
    private VisualElement WinPage;
    private VisualElement GetMoreTimeReword;
    private VisualElement LosePage;
    private GroupBox ItemsGrupPropsGet;
    GroupBox SettingGrop;
    Label MaxPropCanGetText;



    #endregion
    // Start is called before the first frame update


    // Colcolation Days
    #region varibels
    private int TimetoEndGame = 100;
    [SerializeField] int Max_TimetoEndGame = 100;
    public int BoosMony = 240;
    private int totalMony;
    private int Coins;
    #region  SoundVaribals
    [SerializeField] Texture2D[] Sounds;
    [SerializeField] Texture2D[] Music;
    #endregion
    #endregion


    // Elemants


    private void Awake()
    {
        Instance = this;

        /// Gets the root VisualElement from the UIDocument.
        /// This allows access to the UI hierarchy defined in the UIDocument.
        root = uIDocument.rootVisualElement;

        Start_GameBtn = root.Q<Button>("Start_Game");
        WinPage = root.Q<VisualElement>("Win");
        LosePage = root.Q<VisualElement>("Lose");
        TextCountaty = root.Q<Label>("Countaty");
        ItemsGrupPropsGet = root.Q<GroupBox>("ItemsGrupPropsGet");
        MaxPropCanGetText = root.Q<Label>("MaxPropCanGetText");
        SettingGrop = root.Q<GroupBox>("SettingGrop");
        GetMoreTimeReword = root.Q<VisualElement>("GetMoreTimeReword");
    }
    void Start()
    {
        if (GameManager.instance)
        {
            Max_TimetoEndGame = GameManager.instance.LoadData<int>(SaveKeys.Max_TimetoEndGameKey, Max_TimetoEndGame);
            BoosMony = GameManager.instance.LoadData<int>(SaveKeys.BoosMonyKey, BoosMony);
            totalMony = GameManager.instance.LoadData<int>(SaveKeys.Coins, 0);

        }
        TimetoEndGame = Max_TimetoEndGame;

        root.Q<Label>("MonyStartGame").text = totalMony.ToString();

        /// Initializes the UI buttons.
        initilasButons();

        root.Q<Label>("Days").text = $"Time {TimetoEndGame}";

    }

    void initilasButons()
    {
        Start_GameBtn.clicked += () =>
        {
            SoundManager.instance.PlayeWithIndex(0);
            GameManager.instance.is_Play = true;

            root.Q<VisualElement>("StartGame").style.display = DisplayStyle.None;
            root.Q<VisualElement>("GamePage").style.display = DisplayStyle.Flex;


            root.Q<Label>("boosMony").text = $"/{BoosMony}";

            ControllePlayer.SetActive(true);
            StartCoroutine(Calcolation());
            root.Q<Label>("Leveltext").text = "LEVEL " + GameManager.instance.levelIndex.ToString();

        };


        root.Q<Button>("SettingBtn").clicked += () =>
        {
            if (SettingGrop.style.visibility == Visibility.Hidden)
            {
                SettingGrop.style.visibility = Visibility.Visible;
            }
            else
            {
                SettingGrop.style.visibility = Visibility.Hidden;
            }
            SoundManager.instance.PlayeWithIndex(0);

        };


        SettingGrop.Q<Button>("SoundSt").clicked += () =>
        {
            SoundUI();

        };
        SettingGrop.Q<Button>("MusicSt").clicked += () =>
        {
            MusicUI();

        };

        GetMoreTimeReword.Q<Button>("CloseRewodTime").clicked += () =>
        {
            GetMoreTimeReword.RemoveFromClassList("AnimtionWindow");
            GetMoreTimeReword.RegisterCallback<TransitionEndEvent>((TransitionEndEvent) =>
            {
                lose();
            });

        };
        GetMoreTimeReword.Q<Button>("GetRewordTime").clicked += () =>
        {
            Debug.Log("Set Coins ");
            GetMoreTimeReword.RemoveFromClassList("AnimtionWindow");
            TimetoEndGame = 30;
            StartCoroutine(Calcolation());

            // GetMoreTimeReword.RemoveFromClassList("AnimtionWindow");
            // GetMoreTimeReword.RegisterCallback<TransitionEndEvent>((TransitionEndEvent) =>
            // {

            //     GetMoreTimeReword.style.display = DisplayStyle.None;
            // });

        };

    }

    /// <summary>
    /// Toggles the sound on and off.
    /// </summary>
    public void SoundUI()
    {
        if (SoundManager.instance.SoundControlle())
        {
            SettingGrop.Q<Button>("SoundSt").style.backgroundImage = new StyleBackground(Sounds[0]);

        }
        else
        {
            SettingGrop.Q<Button>("SoundSt").style.backgroundImage = new StyleBackground(Sounds[1]);
        }
        SoundManager.instance.PlayeWithIndex(0);
    }

    /// <summary>
    /// Handles updating the UI based on the sound settings.
    /// </summary>
    public void MusicUI()
    {
        if (SoundManager.instance.MusicControlle())
        {
            SettingGrop.Q<Button>("MusicSt").style.backgroundImage = new StyleBackground(Music[0]);

        }
        else
        {
            SettingGrop.Q<Button>("MusicSt").style.backgroundImage = new StyleBackground(Music[1]);
        }
        SoundManager.instance.PlayeWithIndex(0);
    }



    /// <summary>
    /// Sets the coin value and updates the UI.
    /// Checks if coins are below half of boss money and colors text red or green. 
    /// </summary>
    public void SetMony(int value)
    {
        Coins = value;
        TextCountaty.text = Coins.ToString();
        if (value < (BoosMony / 2))
        {

            TextCountaty.style.color = Color.red;
        }
        else
        {
            TextCountaty.style.color = Color.green;
        }
    }

    IEnumerator Calcolation()
    {
        while (true && TimetoEndGame >= 0)
        {
            yield return new WaitForSeconds(1);
            TimetoEndGame -= 1;
            root.Q<Label>("Days").text = $"Time {TimetoEndGame}";
            if (TimetoEndGame <= 0 && GameManager.instance.is_Play)
            {

                ShowRewordTime();
                break;
            }
        }
    }

    /// <summary>
    /// Called when the player wins the game. Handles any win condition logic.
    /// </summary>
    public void win()
    {
        GameManager.instance.is_Play = false;
        WinPage.style.display = DisplayStyle.Flex;
        WinPage.AddToClassList("AnimtionWindow");
        ControllePlayer.SetActive(false);
        root.Q<VisualElement>("GamePage").style.display = DisplayStyle.None;

        int value = 0;
        int valueUpdate = 0;
        DOTween.To(() => value, x => value = x, Coins, 1).SetDelay(1f).OnUpdate(() =>
        {
            valueUpdate = value;
            WinPage.Q<Label>("WinMony").text = valueUpdate.ToString();

        }).OnComplete(() =>
        {
            Coins -= BoosMony;
            totalMony += Coins;
            ES3.Save(SaveKeys.Coins, totalMony);
        });

        ES3.Save(SaveKeys.roomsCountActive, GameManager.instance.roomsCountActive + 1);

        WinPage.Q<Button>("Next").clicked += () =>
        {
            SoundManager.instance.PlayeWithIndex(0);

            ES3.Save(SaveKeys.BoosMonyKey, BoosMony += 10);
            ES3.Save(SaveKeys.Max_TimetoEndGameKey, Max_TimetoEndGame += 5);
            ES3.Save(SaveKeys.LevelIndex, GameManager.instance.levelIndex++);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
        SoundManager.instance.PlayeWithIndex(5);

    }

    /// Called when the player loses the game. Handles any lose condition logic.
    public void lose()
    {
        if (!GameManager.instance.is_Play) return;
        LosePage.style.display = DisplayStyle.Flex;
        LosePage.AddToClassList("AnimtionWindow");
        ControllePlayer.SetActive(false);
        root.Q<VisualElement>("GamePage").style.display = DisplayStyle.None;

        int value = 0;
        int valueUpdate = 0;
        DOTween.To(() => value, x => value = x, Coins, 1).SetDelay(1f).OnUpdate(() =>
        {
            valueUpdate = value;
            LosePage.Q<Label>("WinMony").text = valueUpdate.ToString();

        }).OnComplete(() =>
        {
            Coins -= BoosMony;
            totalMony += Coins;
            ES3.Save(SaveKeys.Coins, totalMony);
        });

        ES3.Save(SaveKeys.roomsCountActive, GameManager.instance.roomsCountActive + 1);
        LosePage.Q<Button>("Next").clicked += () =>
            {
                SoundManager.instance.PlayeWithIndex(0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            };
        GameManager.instance.is_Play = false;
        SoundManager.instance.PlayeWithIndex(6);
    }

    void ShowRewordTime()
    {
        //GetMoreTimeReword.style.display = DisplayStyle.Flex;
        GetMoreTimeReword.AddToClassList("AnimtionWindow");
    }

    // Set Props Image
    public void SetPropsImage(List<propCall> props, bool ClearAll = false)
    {
        ItemsGrupPropsGet.Clear();
        if (props.Count > 0)
        {
            MaxPropCanGetText.style.display = DisplayStyle.Flex;
        }
        else
        {
            MaxPropCanGetText.style.display = DisplayStyle.None;
        }
        MaxPropCanGetText.text = $"{props.Count}/3";

        for (int i = 0; i < props.Count; i++)
        {
            IMGUIContainer Card = new IMGUIContainer();
            Card.AddToClassList("CardItemsGet");
            Card.style.backgroundImage = new StyleBackground(props[i].ItemImage);

            ItemsGrupPropsGet.Add(Card);
        }
        if (ClearAll)
        {
            ItemsGrupPropsGet.Clear();
        }
    }



}





using System.Collections;
using As_Star;
using DG.Tweening;
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
    private Button Start_Game;
    private Label boosMony;
    private Label TextCountaty;
    private VisualElement WinPage;
    #endregion
    // Start is called before the first frame update


    // Colcolation Days
    #region varibels
    private int totalMony;
    [SerializeField] int AllDays = 3;
    public int BoosMony = 240;
    private int Coins;
    #endregion


    // Elemants


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        root = uIDocument.rootVisualElement;

        Start_Game = root.Q<Button>("Start_Game");
        WinPage = root.Q<VisualElement>("Win");
        TextCountaty = root.Q<Label>("Countaty");

        totalMony = GameManager.instance.ChackKey<int>(SaveKeys.Coins);
        root.Q<Label>("MonyStartGame").text=totalMony.ToString();




        Start_Game.clicked += () =>
         {
             SoundManager.instance.PlayeWithIndex(0);
             GameManager.instance.is_Play = true;

             root.Q<VisualElement>("StartGame").style.display = DisplayStyle.None;
             root.Q<VisualElement>("GamePage").style.display = DisplayStyle.Flex;


             root.Q<Label>("boosMony").text = $"/{BoosMony}";

             ControllePlayer.SetActive(true);
             StartCoroutine(Colcolation());
         };

        SetDay(AllDays);

    }

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

    public void SetDay(int value)
    {
        if (value < 0)
        {
            Debug.Log("Game Done");
        }
        root.Q<Label>("Days").text = $"Day {value}";
    }

    IEnumerator Colcolation()
    {
        while (true && AllDays >= 0)
        {
            yield return new WaitForSeconds(30);
            AllDays -= 1;
            SetDay(AllDays);
        }
    }

    public void win()
    {
        WinPage.style.display = DisplayStyle.Flex;
        WinPage.AddToClassList("AnimtionWindow");
        ControllePlayer.SetActive(false);
        root.Q<VisualElement>("GamePage").style.display = DisplayStyle.None;

        Coins -= BoosMony;
        totalMony += Coins;
        ES3.Save(SaveKeys.Coins, totalMony);
        Debug.Log(Coins);
        int value = 0;
        int valueUpdate = 0;
        DOTween.To(() => value, x => value = x, Coins, 1).SetDelay(1f).OnUpdate(() =>
        {
            valueUpdate = value;
            root.Q<Label>("WinMony").text = valueUpdate.ToString();

        });

        root.Q<Button>("Next").clicked += () =>
            {
                SoundManager.instance.PlayeWithIndex(0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            };
    }

}





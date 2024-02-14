using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UI_Game : MonoBehaviour
{
    public static UI_Game Instance;
    [SerializeField] UIDocument uIDocument;

    // Player
    [SerializeField] GameObject ControllePlayer;

    VisualElement root;
    // Start is called before the first frame update


    // Colcolation Days
    [SerializeField] int AllDays = 3;
    public int BoosMony = 240;


    // Elemants
    private Button Start_Game;
    private Label boosMony;
    private Label TextCountaty;
    private VisualElement WinPage;

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




        Start_Game.clicked += () =>
         {
             print("Start Game");

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
        TextCountaty.text = value.ToString();
        if (value < 40)
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
        if (WinPage.ClassListContains("AnimtionWindow"))
        {
            WinPage.RemoveFromClassList("AnimtionWindow");
        }
        WinPage.style.display = DisplayStyle.Flex;
        WinPage.AddToClassList("AnimtionWindow");
        ControllePlayer.SetActive(false);
        root.Q<VisualElement>("GamePage").style.display = DisplayStyle.None;

        root.Q<Button>("Next").clicked += () =>
        {
            // WinPage.style.display = DisplayStyle.None;
            // root.Q<VisualElement>("StartGame").style.display = DisplayStyle.Flex;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
    }



}

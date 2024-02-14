using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        root = uIDocument.rootVisualElement;
        root.Q<Button>("Start_Game").clicked += () =>
        {
            print("Start Game");

            root.Q<VisualElement>("bg").style.visibility = Visibility.Hidden;

            root.Q<VisualElement>("DayTimer").style.visibility = Visibility.Visible;
            ControllePlayer.SetActive(true);
            StartCoroutine(Colcolation());
        };
        SetDay(AllDays);

    }

    public void SetMony(int value)
    {
        Label TextCountaty = root.Q<Label>("Countaty");
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



}

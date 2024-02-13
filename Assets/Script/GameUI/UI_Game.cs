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
            root.Q<Button>("Start_Game").style.visibility = Visibility.Hidden;
            root.Q<VisualElement>("DayTimer").style.visibility = Visibility.Visible;
            ControllePlayer.SetActive(true);
        };

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


}

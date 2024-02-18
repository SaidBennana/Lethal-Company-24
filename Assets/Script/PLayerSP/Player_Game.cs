using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;
using As_Star;
using Unity.VisualScripting;


public class Player_Game : MonoBehaviour
{

    // Props manager
    [SerializeField] int MonyFromProps;
    [SerializeField] List<propCall> Props;// props I get
    // Set Props 
    private Transform PointToSet;

    IEnumerator SetProps()
    {
        if (PointToSet)
        {
            foreach (propCall prop in Props.ToList())
            {
                prop.transform.position = transform.position;
                if (prop.gameObject.TryGetComponent(out MeshCollider mesh))
                {
                    Destroy(mesh);
                }
                prop.gameObject.SetActive(true);

                prop.transform.DOScale(0, 0.4f);
                prop.transform.DOMove(PointToSet.position, 0.4f).OnComplete(() =>
                {
                    if (GameManager.instance.is_Play)
                        SoundManager.instance.PlayeWithIndex(2);
                    DOTween.Kill(prop);
                    Transform IndexProp = prop.transform;
                    Props.Remove(prop);

                    if (prop.TryGetComponent(out propCall propCall))
                    {
                        MonyFromProps += propCall.price;
                        UI_Game.Instance.SetMony(MonyFromProps);
                        if (Props.Count <= 0)
                            if (UI_Game.Instance.BoosMony <= MonyFromProps)
                            {
                                // you win
                                if (GameManager.instance.is_Play)
                                    UI_Game.Instance.win();
                            }
                    }
                    Destroy(IndexProp.gameObject);
                });
                UI_Game.Instance.SetPropsImage(Props);
                yield return new WaitForSeconds(0.5f);

            }
            StartCoroutine(ChackPropsInScnce());
            UI_Game.Instance.SetPropsImage(Props, true);


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Checks if the collided object has the "prop" tag and if so, 
        /// adds it to the Props list and deactivates the object.
        /// </summary>
        if (other.CompareTag("prop"))
        {
            if (Props.Count <= 2)
            {
                if (other.gameObject.TryGetComponent(out MeshCollider mesh))
                {
                    Destroy(mesh);
                }
                Props.Add(other.GetComponent<propCall>());
                other.gameObject.SetActive(false);
                SoundManager.instance.PlayeWithIndex(1);
                UI_Game.Instance.SetPropsImage(Props);
            }
            else
            {
                SoundManager.instance.PlayeWithIndex(3);
            }

        }
        else if (other.CompareTag("setProps"))
        {
            if (Props.Count > 0)
            {
                PointToSet = other.transform;
                StartCoroutine(SetProps());
            }
        }
    }
    IEnumerator ChackPropsInScnce()
    {
        Debug.Log("ChackPropsInScnce 1");
        propCall[] propCall = FindObjectsOfType<propCall>();
        yield return new WaitForSeconds(2);
        if (propCall.Length <= 3)
        {
            spwinPlatform[] _spwinPlatform = FindObjectsOfType<spwinPlatform>();
            foreach (var item in _spwinPlatform)
            {
                item.SetProps();

            }
        }
    }
}


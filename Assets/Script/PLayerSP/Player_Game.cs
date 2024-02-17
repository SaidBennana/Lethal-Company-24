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
    [SerializeField] List<Transform> Props;// props I get
    // Set Props 
    private Transform PointToSet;

    IEnumerator SetProps()
    {
        if (PointToSet)
        {
            foreach (Transform prop in Props.ToList())
            {
                prop.transform.position = transform.position;
                if (prop.gameObject.TryGetComponent(out MeshCollider mesh))
                {
                    Destroy(mesh);
                }
                prop.gameObject.SetActive(true);

                prop.DOScale(0, 0.4f);
                prop.DOMove(PointToSet.position, 0.4f).OnComplete(() =>
                {
                    if (GameManager.instance.is_Play)
                        SoundManager.instance.PlayeWithIndex(2);
                    DOTween.Kill(prop);
                    Transform IndexProp = prop;
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
                                GameManager.instance.is_Play = false;
                            }
                    }
                    Destroy(IndexProp.gameObject);
                });
                yield return new WaitForSeconds(0.5f);

            }
            StopCoroutine(ChackPropsInScnce());
            StartCoroutine(ChackPropsInScnce());

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
            if (other.gameObject.TryGetComponent(out MeshCollider mesh))
            {
                Destroy(mesh);
            }
            Props.Add(other.transform);
            other.gameObject.SetActive(false);
            SoundManager.instance.PlayeWithIndex(1);
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
        yield return new WaitForSeconds(6);
        if (propCall.Length <= 0)
        {
            Debug.Log("ChackPropsInScnce 2");
            spwinPlatform[] _spwinPlatform = FindObjectsOfType<spwinPlatform>();
            foreach (var item in _spwinPlatform)
            {
                item.SetProps();

            }
        }
    }
}


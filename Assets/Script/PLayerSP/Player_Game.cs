using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;


public class Player_Game : MonoBehaviour
{

    // Props manager
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
                    DOTween.Kill(prop);
                    Transform IndexProp = prop;
                    Props.Remove(prop);
                    Destroy(IndexProp.gameObject);
                });
                yield return new WaitForSeconds(0.5f);

            }
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
}

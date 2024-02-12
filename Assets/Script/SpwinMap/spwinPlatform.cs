using System.Collections.Generic;
using UnityEngine;

public class spwinPlatform : MonoBehaviour
{
    [SerializeField] bool is_Spwin = false;
    public List<int> SpwinPoins;
    [SerializeField] GameObject[] Rooms;
    [SerializeField] GameObject[] Cloases;

    // Start is called before the first frame update
    //1=> top
    //2=> bottom
    //3=> right
    //4=> left
    void Start()
    {

        Spwin();
    }


    public void Spwin()
    {
        if (is_Spwin == false)
        {

            for (int i = 0; i < SpwinPoins.Count; i++)
            {
                if (SpwinManager.instance.RoomsCount < 0) return;
                SpwinManager.instance.RoomsCount--;

                int rand = Random.Range(0, Rooms.Length);
                GameObject bb = Instantiate(Rooms[rand]);

                spwinPlatform spwin = bb.GetComponent<spwinPlatform>();

                bb.transform.SetParent(transform.parent);


                if (SpwinPoins[i] == 1 && spwin.SpwinPoins.Exists(x => x == 2))
                {
                    Post(bb, new Vector3(0, 0, 20), 2);
                    SpwinPoins.Remove(1);
                }
                else if (SpwinPoins[i] == 2 && spwin.SpwinPoins.Exists(x => x == 1))
                {
                    Post(bb, new Vector3(0, 0, -20), 1);
                    SpwinPoins.Remove(2);
                }
                else if (SpwinPoins[i] == 3 && spwin.SpwinPoins.Exists(x => x == 4))
                {
                    Post(bb, new Vector3(30, 0, 0), 4);
                    SpwinPoins.Remove(3);
                }
                else if (SpwinPoins[i] == 4 && spwin.SpwinPoins.Exists(x => x == 3))
                {
                    Post(bb, new Vector3(-30, 0, 0), 3);
                    SpwinPoins.Remove(4);
                }
                else
                {
                    Destroy(bb);
                    SpwinManager.instance.RoomsCount++;
                }
            }
            if (SpwinManager.instance.RoomsCount > 0)
            {
                Spwin();
            }
            else
            {

                is_Spwin = true;
            }
        }


    }

    void CloasWall()
    {
        for (int i = 0; i < SpwinPoins.Count; i++)
        {

            if (SpwinPoins[i] == 1)
            {
                Instantiate(Cloases[0], transform.position, Quaternion.identity);
            }
            else if (SpwinPoins[i] == 2)
            {
                Instantiate(Cloases[1], transform.position, Quaternion.identity);
            }
            else if (SpwinPoins[i] == 3)
            {
                Instantiate(Cloases[2], transform.position, Quaternion.identity);
            }
            else if (SpwinPoins[i] == 4)
            {
                Instantiate(Cloases[3], transform.position, Quaternion.identity);
            }
        }
    }

    void Post(GameObject bb, Vector3 vv, int index)
    {
        bb.GetComponent<spwinPlatform>().SpwinPoins.Remove(index);
        bb.transform.position = transform.position + vv;
    }
}




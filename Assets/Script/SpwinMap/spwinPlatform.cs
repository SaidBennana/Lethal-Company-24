using System.Collections.Generic;
using UnityEngine;

public class spwinPlatform : MonoBehaviour
{
    [SerializeField] bool is_Spwin = false;
    public List<int> SpwinPoins;
    [SerializeField] GameObject[] Rooms;
    [SerializeField] GameObject[] Cloases;

    [Header("Props")]
    [SerializeField] Transform[] Points;// Spwin Points instanction;
    private Transform[] popes;

    // Start is called before the first frame update
    //1=> top
    //2=> bottom
    //3=> right
    //4=> left
    void Start()
    {

        Spwin();
        popes = SpwinManager.instance.props;

        /// Instantiates a random prop prefab at each point in the Points array. 
        /// The prop prefab is selected randomly from the popes array.
        /// The instantiated prop is scaled to 2x default size.
        /// This populates the map with random props at the predefined points.
        for (int i = 0; i < Points.Length; i++)
        {
            Transform ff = Instantiate(popes[Random.Range(0, popes.Length)], Points[i].position, Quaternion.identity);
            ff.localScale = Vector3.one * 2;
        }
    }


    public void Spwin()
    {
        if (is_Spwin == false)
        {

            for (int i = 0; i < SpwinPoins.Count; i++)
            {
                /// Decrements the RoomsCount property on the SpwinManager singleton instance, but only if its value is greater than 0.
                /// This prevents the count from going below 0.
                if (SpwinManager.instance.RoomsCount < 0) return;
                SpwinManager.instance.RoomsCount--;

                /// Instantiates a random room prefab from the Rooms array
                /// and stores it in the bb variable.
                /// Uses Random.Range to get a random index for the room prefab.
                int rand = Random.Range(0, Rooms.Length);
                GameObject bb = Instantiate(Rooms[rand]);

                /// Gets the spwinPlatform component from the GameObject bb.
                /// This allows accessing the spwinPlatform component to configure it after instantiating the prefab.
                spwinPlatform spwin = bb.GetComponent<spwinPlatform>();

                bb.transform.SetParent(transform.parent);

                /// Checks if the current SpwinPoin matches an existing point on the spwinPlatform component. 
                /// If there is a match, instantiates a room at the appropriate relative position and removes the point from SpwinPoins.
                /// If there is no match, destroys the room and returns it to the pool.
                /// This handles matching points between platforms and instantiating connected rooms.

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
            /// Checks if there are rooms left to spawn. 
            /// If so, calls Spwin() to spawn more rooms.
            /// Otherwise sets is_Spwin to true to indicate no more rooms should spawn.
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



    void Post(GameObject bb, Vector3 vv, int index)
    {
        bb.GetComponent<spwinPlatform>().SpwinPoins.Remove(index);
        bb.transform.position = transform.position + vv;
    }
}




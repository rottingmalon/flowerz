using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    [SerializeField] private GameObject redFlower;
    [SerializeField] private GameObject blueFlower;
    [SerializeField] private GameObject greenFlower;

    private int SelectedFlower = 1;

    void Update()
    {
        SelectedFlower = CheckFlower();

        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit) && hit.transform.tag == "Ground") 
            {
                switch (SelectedFlower) 
                {
                    case 1:
                        GameObject redTemp = Instantiate(redFlower, hit.point, Quaternion.identity);
                        redTemp.AddComponent<flower>();
                        redTemp.GetComponent<flower>().fuseRadius = 5;
                        if (redTemp.GetComponent<flower>().growAmount == 100) 
                        {
                            CheckFusions(redTemp);
                        }
                        break;
                    case 2:
                        GameObject blueTemp = Instantiate(blueFlower, hit.point, Quaternion.identity);
                        blueTemp.AddComponent<flower>();
                        break;
                    case 3:
                        GameObject greenTemp = Instantiate(greenFlower, hit.point, Quaternion.identity);
                        greenTemp.AddComponent<flower>();
                        break;
                    default: 
                        break;
                }
            }
        }
    }

    private void CheckFusions(GameObject newFlower) 
    {
        // get the radius & position
        float newRadius = newFlower.GetComponent<flower>().fuseRadius;
        Transform newTransform = newFlower.transform;

        //create flower list
        List<GameObject> overlappedFlowers = new List<GameObject>();

        //overlapsphere to make list of nearby flowers

        //compute distance between flowers

        //fuse with nearest fully grown
    }

    private int CheckFlower() 
    {
        int i = SelectedFlower;

        if (Input.GetKeyUp("1") && i != 1)
        {
            i = 1;
            Debug.Log("1");
        }
        else if (Input.GetKeyUp("2") && i != 2)
        {
            i = 2;
            Debug.Log("2");
        }
        else if (Input.GetKeyUp("3") && i != 3)
        {
            i = 3;
            Debug.Log("3");
        }

        return (i);
    }
}

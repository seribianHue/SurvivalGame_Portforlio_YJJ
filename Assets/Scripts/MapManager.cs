using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public enum eREGION { ePlain, eMountain, eSnow, eForest };

    [Header("Current Map Size")]
    [SerializeField] int _curMapRadius = 20;
    
    [Header("Resources Prefab")]
    [SerializeField] GameObject _tree;
    [SerializeField] GameObject _rock;
    [SerializeField] GameObject _grass;

    [Header("Resources Materials")]
    [SerializeField] Material[] _materials;

    private void Awake()
    {
        MakeRegion(eREGION.ePlain);
        MakeRegion(eREGION.eSnow);
        MakeRegion(eREGION.eForest);
        MakeRegion(eREGION.eMountain);
    }

    List<int[]> _plainResorcesList = new List<int[]>();
    List<int[]> _forestResorcesList = new List<int[]>();
    List<int[]> _mountainResourcesList = new List<int[]>();
    List<int[]> _snowResourcesList = new List<int[]>();

    void MakeRegion(eREGION eRegion)
    {
        switch (eRegion)
        {
            case eREGION.ePlain:
                for(int i = 0; i < 20; ++i)
                {
                    int X, Y;
                    if (CommMath.Instance.ProbabilityMethod(20))
                    {
                        GetResourcePos(_curMapRadius, _plainResorcesList, out X, out Y);
                        GameObject treeObj = Instantiate(_tree);
                        treeObj.transform.position = new Vector3(X, 0, -Y);
                        treeObj.GetComponent<Renderer>().material = _materials[0];
                    }
                    else if (CommMath.Instance.ProbabilityMethod(80))
                    {
                        GetResourcePos(_curMapRadius, _plainResorcesList, out X, out Y);
                        GameObject grassObj = Instantiate(_grass);
                        grassObj.transform.position = new Vector3(X, 0, -Y);
                        grassObj.GetComponent<Renderer>().material = _materials[0];
                    }
                    else
                    {
                        GetResourcePos(_curMapRadius, _plainResorcesList, out X, out Y);
                        GameObject rockObj = Instantiate(_rock);
                        rockObj.transform.position = new Vector3(X, 0, -Y);
                    }
                }
                break;

            case eREGION.eMountain:
                for (int i = 0; i < 20; ++i)
                {
                    int X, Y;
                    GetResourcePos(_curMapRadius, _mountainResourcesList, out X, out Y);
                    GameObject rockObj = Instantiate(_rock);
                    rockObj.transform.position = new Vector3(-X, 0, -Y);
                }
                break;

            case eREGION.eSnow:
                for (int i = 0; i < 20; ++i)
                {
                    int X, Y;
                    if (CommMath.Instance.ProbabilityMethod(95))
                    {
                        GetResourcePos(_curMapRadius, _snowResourcesList, out X, out Y);
                        GameObject treeObj = Instantiate(_tree);
                        treeObj.transform.position = new Vector3(X, 0, Y);
                        treeObj.GetComponent<Renderer>().material = _materials[2];

                    }
                    else
                    {
                        GetResourcePos(_curMapRadius, _snowResourcesList, out X, out Y);
                        GameObject rockObj = Instantiate(_rock);
                        rockObj.transform.position = new Vector3(X, 0, Y);
                    }
                }
                break;

            case eREGION.eForest:
                for (int i = 0; i < 20; ++i)
                {
                    int X, Y;
                    if (CommMath.Instance.ProbabilityMethod(95))
                    {
                        GetResourcePos(_curMapRadius, _forestResorcesList, out X, out Y);
                        GameObject treeObj = Instantiate(_tree);
                        treeObj.transform.position = new Vector3(-X, 0, Y);
                        treeObj.GetComponent<Renderer>().material = _materials[3];
                    }
                    else
                    {
                        GetResourcePos(_curMapRadius, _forestResorcesList, out X, out Y);
                        GameObject rockObj = Instantiate(_rock);
                        rockObj.transform.position = new Vector3(-X, 0, Y);
                    }
                }
                break;
        }

    }

    void GetResourcePos(int maxValule, List<int[]> resourceList, out int X, out int Y)
    {
        int[] ranPos = new int[2];
        ranPos[0] = Random.Range(1, maxValule);
        ranPos[1] = Random.Range(1, maxValule);

        X = ranPos[0]; Y = ranPos[1];

        if (resourceList.Count == 0) { resourceList.Add(ranPos); return; }

        else
        {
            //Check X, Y
            bool isOk = true;
            do
            {
                isOk = true;
                foreach (int[] pos in resourceList)
                {
                    if ((pos[0] < ranPos[0] + 3 && pos[0] > ranPos[0] - 3) 
                        && (pos[1] < ranPos[1] + 3 && pos[1] > ranPos[1] - 3))
                    {
                        isOk = false;
                        ranPos[0] = Random.Range(1, maxValule);
                        ranPos[1] = Random.Range(1, maxValule);
                        break;
                    }
                }
            } while (isOk == false);
            
            resourceList.Add(ranPos);
            X = ranPos[0]; Y = ranPos[1];
            return;
        }
    }
}

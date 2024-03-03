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
                    int X = 0, Y = 0;
                    if (CommMath.Instance.ProbabilityMethod(20))
                    {
                        SetTree(X, Y, _plainResorcesList, 1);
                    }
                    else if (CommMath.Instance.ProbabilityMethod(80))
                    {
                        SetGrass(X, Y);
                    }
                    else
                    {
                        SetRock(X, Y, _plainResorcesList);
                    }
                }
                break;

            case eREGION.eMountain:
                for (int i = 0; i < 20; ++i)
                {
                    int X = 0, Y = 0;
                    SetRock(X, Y, _mountainResourcesList);
                }
                break;

            case eREGION.eSnow:
                for (int i = 0; i < 20; ++i)
                {
                    int X = 0, Y = 0;
                    if (CommMath.Instance.ProbabilityMethod(95))
                    {
                        SetTree(X, Y, _snowResourcesList, 3);
                    }
                    else
                    {
                        SetRock(X, Y, _snowResourcesList);
                    }
                }
                break;

            case eREGION.eForest:
                for (int i = 0; i < 20; ++i)
                {
                    int X = 0, Y = 0;
                    if (CommMath.Instance.ProbabilityMethod(95))
                    {
                        SetTree(X, Y, _forestResorcesList, 2);
                    }
                    else
                    {
                        SetRock(X, Y, _forestResorcesList);
                    }
                }
                break;
        }

    }

    void SetTree(int X, int Y, List<int[]> region, int treeIndex)
    {
        GetResourcePos(_curMapRadius, region, out X, out Y);
        GameObject treeObj = Instantiate(_tree);
        
        if(region == _plainResorcesList)
            treeObj.transform.position = new Vector3(X, 0, -Y);
        else if(region == _forestResorcesList)
            treeObj.transform.position = new Vector3(-X, 0, Y);
        else if(region == _snowResourcesList)
            treeObj.transform.position = new Vector3(X, 0, Y);
        else
            treeObj.transform.position = new Vector3(-X, 0, -Y);


        treeObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        float scale = Random.Range(0.7f, 1.5f);
        treeObj.transform.localScale = new Vector3(scale, scale, scale);
        treeObj.GetComponent<Tree>().ChangeTreeLook(treeIndex);
    }
    void SetRock(int X, int Y, List<int[]> region)
    {
        GetResourcePos(_curMapRadius, _forestResorcesList, out X, out Y);
        GameObject rockObj = Instantiate(_rock);

        if (region == _plainResorcesList)
            rockObj.transform.position = new Vector3(X, 0, -Y);
        else if (region == _forestResorcesList)
            rockObj.transform.position = new Vector3(-X, 0, Y);
        else if (region == _snowResourcesList)
            rockObj.transform.position = new Vector3(X, 0, Y);
        else
            rockObj.transform.position = new Vector3(-X, 0, -Y);
        
        rockObj.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        rockObj.GetComponent<Rock>().SetRockLooks();
    }
    void SetGrass(int X, int Y)
    {
        GetResourcePos(_curMapRadius, _plainResorcesList, out X, out Y);
        GameObject grassObj = Instantiate(_grass);
        grassObj.transform.position = new Vector3(X, 0, -Y);
        grassObj.GetComponent<Renderer>().material = _materials[0];
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

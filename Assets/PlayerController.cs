using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject UnitPrefab;
    [SerializeField] private int UnitsNumber = 10;

    private List<GameObject> UnitsAlive = new List<GameObject>();

    private BoxCollider Box;
    private float BoxSizeX;
    private float BoxSizeZ;

    private List<Vector3> NewUnitsPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Box = GetComponent<BoxCollider>();
        BoxSizeX = Box.size.x;
        BoxSizeZ = Box.size.z;

        float OffSet = Box.size.x / UnitsNumber;

        for (int i = 0; i < UnitsNumber; i++)
        {
            GameObject CurrentUnit = Instantiate(UnitPrefab, transform.position - new Vector3((Box.size.x / 2), 0, 0) + new Vector3(OffSet * i, 0, 0), transform.rotation);
            CurrentUnit.transform.SetParent(transform);
            UnitsAlive.Add(CurrentUnit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (NewUnitsPositions.Count == UnitsAlive.Count)
        {
            for (int i = 0; i < UnitsAlive.Count; i++)
            {
                UnitsAlive[i].transform.localPosition = Vector3.MoveTowards(UnitsAlive[i].transform.localPosition, NewUnitsPositions[i], 0.1f);
            }
        }
    }

    public void OrderUnits(List<float> XPath, List<float> ZPath)
    {
        int ListsDifferenceCoefficient = XPath.Count / UnitsAlive.Count;

        NewUnitsPositions = new List<Vector3>();

        for (int i = 0; i < UnitsAlive.Count; i++)
        {
            if (ListsDifferenceCoefficient >= 1)
            {
                int PathIndex = i * ListsDifferenceCoefficient;
                NewUnitsPositions.Add((new Vector3(XPath[PathIndex] * BoxSizeX, 0, ZPath[PathIndex] * BoxSizeZ) - new Vector3(BoxSizeX / 2, 0, BoxSizeZ / 2)));
            }
            else if (i < XPath.Count)
            {
                NewUnitsPositions.Add((new Vector3(XPath[i] * BoxSizeX, 0, ZPath[i] * BoxSizeZ) - new Vector3(BoxSizeX / 2, 0, BoxSizeZ / 2)));
            }
            else
            {
                NewUnitsPositions.Add((new Vector3(XPath[XPath.Count - 1] * BoxSizeX, 0, ZPath[XPath.Count - 1] * BoxSizeZ) - new Vector3(BoxSizeX / 2, 0, BoxSizeZ / 2)));
            }
        }
    }

    public void AddUnit(Vector3 position)
    {
        GameObject NewUnit = Instantiate(UnitPrefab, position, transform.rotation);
        NewUnit.transform.SetParent(transform);
        UnitsAlive.Add(NewUnit);
    }

    public void RemoveUnit(GameObject Unit)
    {
        UnitsAlive.Remove(Unit);
        Destroy(Unit);
    }
}

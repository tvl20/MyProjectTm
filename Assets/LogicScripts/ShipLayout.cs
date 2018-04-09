using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLayout : MonoBehaviour
{
    public int SizeX;
    public int SizeY;
    public float TileOffset;
    [SerializeField, HideInInspector] private bool mapIsGenerated;
    [SerializeField, HideInInspector] private bool crewIsGenerated;
    public Array2DCell Layout;

    // The prefab used for one crew member
    // This prefab will be used for all the crewmembers, this might be changed int the future
    public GameObject CrewMemberPrefab;

    // Collection of all the crew on the ship
    public List<CrewMember> Crew = new List<CrewMember>();

    // These are the prefabs made in the editor for the tiles with a module
    public GameObject EmptyRoomPrefab;
    public GameObject WeaponsModuleRoomPrefab;
    public GameObject SensorsModuleRoomPrefab;
    public GameObject EnginesModuleRoomPrefab;
    public GameObject ShieldsModuleRoomPrefab;

    void Start()
    {
        GenerateDemo1Map();
        generateDemo1Crew();
    }

[ContextMenu("GenerateDemoMap")]
    public void GenerateDemo1Map()
    {
        if (mapIsGenerated) return;
        mapIsGenerated = true;
        /*
		Ship layout:
		W = weapons
		Sh = shields
		E = engines
		Se = sensors
		X = empty room
		
		W	X	E
		X	X	X
		Se	X	Sh
		 */

        SizeX = 3;
        SizeY = 3;
        Layout = new Array2DCell(SizeX, SizeY);

        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                float xPos = x + x * TileOffset;
                float yPos = y + y * TileOffset;

                Cell createdCell;
                if (x == 0 && y == 0)
                {
                    createdCell = Instantiate(WeaponsModuleRoomPrefab, new Vector3(xPos, 0, yPos) + this.transform.position, Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                else if (x == 2 && y == 0)
                {
                    createdCell = Instantiate(EnginesModuleRoomPrefab, new Vector3(xPos, 0, yPos) + this.transform.position, Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                else if (x == 0 && y == 2)
                {
                    createdCell = Instantiate(SensorsModuleRoomPrefab, new Vector3(xPos, 0, yPos) + this.transform.position, Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                else if (x == 2 && y == 2)
                {
                    createdCell = Instantiate(ShieldsModuleRoomPrefab, new Vector3(xPos, 0, yPos) + this.transform.position, Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                else
                {
                    createdCell = Instantiate(EmptyRoomPrefab, new Vector3(xPos, 0, yPos) + this.transform.position, Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                
                Layout.SetValue(x, y, createdCell);

                Layout.GetValue(x, y).X = x;
                Layout.GetValue(x, y).Y = y;
            }
        }
    }

[ContextMenu("DestroyMap")]
    public void DestroyMap()
    {
        for (int i = this.transform.childCount -1; i >= 0; i--)
        {
            if(this.transform.GetChild(i).GetComponent<Cell>() != null)
            {
#if UNITY_EDITOR
                DestroyImmediate(this.transform.GetChild(i).gameObject);
#else
                Destroy(this.transform.GetChild(i).gameObject);
#endif
            }
        }
        mapIsGenerated = false;
    }

[ContextMenu("GenerateCrew")]
    public void generateDemo1Crew()
    {
        if (crewIsGenerated) return;
        crewIsGenerated = true;

        createCrewMember(0, 2);
        createCrewMember(2, 0);
    }

[ContextMenu("DestroyCrew")]
    public void DestroyCrew()
    {
        for (int i = Crew.Count; i >= 0; i--)
        {
#if UNITY_EDITOR
                DestroyImmediate(Crew[i].gameObject);
#else
                Destroy(Crew[i].gameObject);
#endif
        }

        crewIsGenerated = false;
    }

    // With these methods other classes can easily get the different modules installed by name
    public Module GetInstalledModule(string moduleName)
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                Module foundModule = Layout.GetValue(x, y).Module;
                if (foundModule != null && foundModule.Name == moduleName)
                {
                    return foundModule;
                }
            }
        }
        return null;
    }

    private void createCrewMember(int x, int y)
    {
        CrewMember createdCrewMember = Instantiate(CrewMemberPrefab, 
                                                    Layout.GetValue(x, y).transform.position, 
                                                    Quaternion.identity, 
                                                    Layout.GetValue(x, y).transform
                                                    ).GetComponent<CrewMember>();
        Layout.GetValue(x, y).crewMember = createdCrewMember;
        Crew.Add(createdCrewMember);
    }
}

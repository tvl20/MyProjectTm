using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLayout : MonoBehaviour
{
    public int SizeX;
    public int SizeY;
    public float TileOffset;
    public Cell[,] Layout;

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

    // These are the specific Modules that are installed
    // The Ship can call for these when doing calculations
    public Module WeaponsModule;
    public Module SensorsModule;
    public Module EnginesModule;
    public Module ShieldsModule;

    void Start()
    {
        GenerateDemo1Map();
        generateDemo1Crew();
    }

    // public Cell GetCell(int x, int y)
    // {
    //     if (x >= SizeX ||
    //         y >= SizeY ||
    //         x < 0 ||
    //         y < 0)
    //     {
    //         Debug.LogError("Out of bounds!");
    //         return null;
    //     }

    //     return Layout[x,y];
    // }

    private void GenerateDemo1Map()
    {
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
        Layout = new Cell[SizeX, SizeY];

        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                float xPos = x + x * TileOffset;
                float yPos = y + y * TileOffset;

                if (x == 0 && y == 0)
                {
                    Cell WeaponsCell = Instantiate(WeaponsModuleRoomPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, this.transform).GetComponent<Cell>();
                    Layout[x, y] = WeaponsCell;
                    WeaponsModule = WeaponsCell.Module;
                }
                else if (x == 2 && y == 0)
                {
                    Cell EnginesCell = Instantiate(EnginesModuleRoomPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, this.transform).GetComponent<Cell>();
                    Layout[x, y] = EnginesCell;
                    EnginesModule = EnginesCell.Module;
                }
                else if (x == 0 && y == 2)
                {
                    Cell SensorsCell = Instantiate(SensorsModuleRoomPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, this.transform).GetComponent<Cell>();
                    Layout[x, y] = SensorsCell;
                    SensorsModule = SensorsCell.Module;
                }
                else if (x == 2 && y == 2)
                {
                    Cell ShieldsCell = Instantiate(ShieldsModuleRoomPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, this.transform).GetComponent<Cell>();
                    Layout[x, y] = ShieldsCell;
                    ShieldsModule = ShieldsCell.Module;
                }
                else
                {
                    Layout[x, y] = Instantiate(EmptyRoomPrefab, new Vector3(xPos, 0, yPos), Quaternion.identity, this.transform).GetComponent<Cell>();
                }
                Layout[x, y].X = x;
                Layout[x, y].Y = y;
            }
        }
    }

    private void generateDemo1Crew()
    {
        createCrewMember(0, 2);
        createCrewMember(2, 0);
    }

    private void createCrewMember(int x, int y)
    {
        CrewMember createdCrewMember = Instantiate(CrewMemberPrefab, Layout[x, y].transform.position, Quaternion.identity, Layout[x, y].transform).GetComponent<CrewMember>();
        Layout[x, y].crewMember = createdCrewMember;
        Crew.Add(createdCrewMember);
    }
}

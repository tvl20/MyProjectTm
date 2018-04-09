using UnityEngine;

//This Generic class is a 'template' that the compiler can create classes from.
//On compile, it analyzes your code and creates a version for each <Type> argument it can be called with.
public class SerializableArray2D<T>
{
	public int sizeX;
	public int sizeY;
	
	[SerializeField] private T[] array; // Unity can serialize a single array fine, so that's what we'll use as backing field

	
	public SerializableArray2D(int sizeX, int sizeY)
	{
		this.sizeX = sizeX;
		this.sizeY = sizeY;
		array = new T[sizeX * sizeY];
	}

	public T GetValue(int x, int y)
	{
		if (x >= sizeX || y >= sizeY)
		{
			Debug.LogError("Value out of range!");
			return default(T);
		}

		return (array[x + (y * sizeX)]);
	}

	public void SetValue(int x, int y, T value)
	{
		array[x + (y * sizeX)] = value;
	}
}

// In order to make our generic array play nice with the Unity Serializer[1], we have to define a non-generic class (without <T>) for it.
// it has to 'exist' ahead of time for Unity to understand how to read & write it.
[System.Serializable] // Mark Serializable
public class Array2DCell : SerializableArray2D<Cell>
{
	public Array2DCell(int sizeX, int sizeY) : base(sizeX, sizeY) {} // Forward constructor to base.
}
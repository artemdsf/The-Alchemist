using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

	public int GetActiveObjects()
	{
		int count = 0;
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (pooledObjects[i].activeInHierarchy)
			{
				count++;
			}
		}
		return count;
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < pooledObjects.Count; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}
		return AddPooledObject();
	}

	private void Start()
	{
		pooledObjects = new List<GameObject>();
		for (int i = 0; i < amountToPool; i++)
		{
			AddPooledObject();
		}
	}
	
	private GameObject AddPooledObject()
	{
		GameObject tmp = Instantiate(objectToPool, transform);
		tmp.SetActive(false);
		pooledObjects.Add(tmp);
		return tmp;
	}
}

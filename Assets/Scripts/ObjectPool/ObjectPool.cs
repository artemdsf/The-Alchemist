using System.Collections.Generic;
using UnityEngine;
public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

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
		pooledObjects.Add(tmp);
		tmp.SetActive(false);
		return tmp;
	}
}

// class origially written by invulse from the Unity forums and modified thereafter, all credit due!

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	//Singleton design pattern
	public static ObjectPool instance;

	// The object prefabs which the pool can handle.
	public GameObject[] objectPrefabs;

	// The pooled objects currently available.
	public List<GameObject>[] pooledObjects;

	// The amount of objects of each type to buffer.
	public int[] amountToBuffer;
	public int defaultBufferAmount = 3;

	// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.

	protected GameObject containerObject;
	
	void Awake () {	

		instance = this;
	}
	
	void Start () {
		
		this.containerObject = new GameObject("ObjectPool");

		//Loop through the object prefabs and make a new list for each one.
		//We do this because the pool can only support prefabs set to it in the editor,
		//so we can assume the lists of pooled objects are in the same order as object prefabs in the array
		
		this.pooledObjects = new List<GameObject>[objectPrefabs.Length];

		int i = 0;		
		foreach ( GameObject objectPrefab in this.objectPrefabs ) {
			this.pooledObjects[i] = new List<GameObject>();  
			int bufferAmount;

			if (i < amountToBuffer.Length) bufferAmount = amountToBuffer[i];
			
			else bufferAmount = defaultBufferAmount;
			
			for (int n=0; n<bufferAmount; n++) {
				GameObject newObj = Instantiate(objectPrefab) as GameObject;
				newObj.name = objectPrefab.name;
				PoolObject(newObj);
			}
			
		i++;
			
		}
	}
	// Gets a new object for the name type provided.  
	//	If no object type exists or if onlypooled is true and there is no objects of that type in the pool
	// then null will be returned.

	// returns the object for type
	
	// If true, it will only return an object if there is one currently pooled.
	
	public GameObject GetObjectForType (string objectType , bool onlyPooled) {
		
		for (int i = 0; i < this.objectPrefabs.Length; i++) {

			GameObject prefab = this.objectPrefabs[i];

			if (prefab.name == objectType) {

					if (this.pooledObjects[i].Count > 0) {
					
					GameObject pooledObject = this.pooledObjects[i][0];
					
					pooledObjects[i].RemoveAt(0);
					
					pooledObject.transform.parent = null;
					
					pooledObject.SetActive(true);

					return pooledObject;

				} else if(!onlyPooled) {
					
					return Instantiate(objectPrefabs[i]) as GameObject;
				}
				break;
			}
		}
	
		//If we have gotten here either there was no object of the specified type or non were left in the pool with onlyPooled set to true
		
		return null;
		
	}

	// Pools the object specified.  Will not be pooled if there is no prefab of that type.

	public void PoolObject ( GameObject obj ) {
		
		for ( int i = 0; i < this.objectPrefabs.Length; i++) {
			
			if (objectPrefabs[i].name == obj.name) {
				
				obj.SetActive(false);
				obj.transform.parent = containerObject.transform;
				this.pooledObjects[i].Add(obj);
				
				return;	
			}	
		}
	}
}
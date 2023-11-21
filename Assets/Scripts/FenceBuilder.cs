using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBuilder : MonoBehaviour
{
    public GameObject objectToCopy; // The GameObject with the MeshFilter you want to copy
    public int numberOfCopies = 5; // Number of copies to make
    public float distanceBetweenCopies = 1.0f; // Distance between each copy

    void Start()
    {
        CopyAndPlaceMeshes();
    }

    void CopyAndPlaceMeshes()
    {
        MeshFilter originalMeshFilter = objectToCopy.GetComponent<MeshFilter>();

        if (originalMeshFilter != null)
        {
            // Calculate the total width based on the number of copies and the distance between them
            float totalWidth = (numberOfCopies - 1) * distanceBetweenCopies;

            // Calculate the starting position to place the copies centrally
            Vector3 startingPosition = transform.position - transform.right * totalWidth / 2;

            for (int i = 0; i < numberOfCopies; i++)
            {
                GameObject newObject = Instantiate(objectToCopy, transform);

                // Calculate the position for the new copy
                Vector3 newPosition = startingPosition + (transform.right * distanceBetweenCopies * i);

                newObject.transform.position = newPosition;
            }
        }
        else
        {
            Debug.LogError("MeshFilter not found on the object to copy.");
        }
    }
}
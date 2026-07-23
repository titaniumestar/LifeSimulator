using UnityEngine;
using static FunctionBox;

public class Gene : MonoBehaviour
{
    public float mutationRate = 1;
    public float movementSpeed = 1;
    public float rotationSpeed = 100;

    float GeneMutation(float geneValue, float mutationRate)
    {
        float mutationValue = Random.Range(
            geneValue - mutationRate * geneValue / 10,
            geneValue + mutationRate * geneValue / 10
            );
        return mutationValue;
    }

    public void cellDivision(GameObject thisGameObject)
    {
        GameObject newCell = Instantiate(
            thisGameObject,
            RandomAroundRangeVector3(
                thisGameObject.transform.position,
                0.2f,
                GameManager.Instance.WorldMinRange,
                GameManager.Instance.WorldMaxRange
                ),
            thisGameObject.transform.rotation
            );
        newCell.name = thisGameObject.name;

        Gene originalGene = thisGameObject.GetComponent<Gene>();
        Gene newGene = newCell.GetComponent<Gene>();

        newGene.movementSpeed = GeneMutation(originalGene.movementSpeed, mutationRate);
        newGene.rotationSpeed = GeneMutation(originalGene.rotationSpeed, mutationRate);

        newCell.GetComponent<FindingFood>().StartWandering();
        thisGameObject.GetComponent<FindingFood>().StartWandering();
    }
}

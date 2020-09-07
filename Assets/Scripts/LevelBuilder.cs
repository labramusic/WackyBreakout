using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject Paddle;
    public GameObject StandardBlock;
    public GameObject BonusBlock;
    public GameObject PickupBlock;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Paddle);
        InstantiateBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InstantiateBlocks()
    {
        // all blocks are same size
        var pos = StandardBlock.transform.position;
        var width = StandardBlock.GetComponent<BoxCollider2D>().size.x * transform.localScale.x;

        // 3 rows
        for (int i = 0; i < 3; ++i)
        {
            // 9 blocks
            for (int j = 0; j < 9; ++j)
            {
                Instantiate(ChooseBlock(), new Vector2(pos.x + j * (width + 0.5f), pos.y + i * (width + 0.5f)), Quaternion.identity);
                //var block = Instantiate(PickupBlock, new Vector2(pos.x + j * (width + 0.5f), pos.y + i * (width + 0.5f)), Quaternion.identity);
                //block.GetComponent<PickupBlock>().PickupEffect = PickupEffect.Speedup;
            }
        }
    }

    private GameObject ChooseBlock()
    {
        float r = Random.value;
        float prob = ConfigurationUtils.StandardBlockProb;
        if (r <= prob)
            return StandardBlock;
        else
        {
            prob += ConfigurationUtils.BonusBlockProb;
            if (r <= prob)
                return BonusBlock;
            else
            {
                GameObject block = PickupBlock;
                prob += ConfigurationUtils.PickupBlockProb;
                if (r <= prob)
                {
                    block.GetComponent<PickupBlock>().PickupEffect = PickupEffect.Freezer;
                    return block;
                }
                else
                {
                    prob += ConfigurationUtils.PickupBlockProb;
                    if (r <= prob)
                    {
                        block.GetComponent<PickupBlock>().PickupEffect = PickupEffect.Speedup;
                        return block;
                    }
                }
            }
        }
        return StandardBlock;
    }
}

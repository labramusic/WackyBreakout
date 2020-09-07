using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    [SerializeField]
    private Sprite StandardBlockSprite1;

    [SerializeField]
    private Sprite StandardBlockSprite2;

    [SerializeField]
    private Sprite StandardBlockSprite3;

    private List<Sprite> Sprites;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Points = ConfigurationUtils.StandardBlockPoints;
        Sprites = new List<Sprite> { StandardBlockSprite1, StandardBlockSprite2, StandardBlockSprite3 };
        GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, 3)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

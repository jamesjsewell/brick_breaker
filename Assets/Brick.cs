using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points = 1;
    public int hitsToBreak;
    public Sprite hitSprite;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BreakBrick() {
        hitsToBreak--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }


}

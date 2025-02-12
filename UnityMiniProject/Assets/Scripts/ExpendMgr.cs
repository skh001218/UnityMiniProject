using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpendMgr : MonoBehaviour
{
    private int baseMaxRowCount = 5;
    private int baseMaxColCount = 5;
    private Vector2 nextBasePos;

    public CreateItem createItem;
    public GameManager gm;
    public SellAreaMgr sellAreaMgr;
    public GameObject playUi;
    public StandMgr standMgr;

    public ItemBase basePrefebs;
    public AddItem baseAddPrefebs;
    private AddItem addBase;

    public Furnace furnacePrefebs;
    public Furnace firstFurnace;
    public AddItem furnaceAddPrefebs;
    public BakeTimeCircle bakeTimeCircle;

    private Vector2 nextFurnacePos;
    private AddItem addFurnace;
    private int furnaceMaxRowCount = 2;
    private int furnaceMaxColCount = 5;
    private int furnaceCount = 1;

    public Stand standPrefebs;
    public Stand firstStand;
    public AddItem standAddPrefebs;
    public GameObject wayPointList;
    private AddItem addStand;
    private Vector2 nextStandPos;
    private float mag = 45f;
    GameObject addWayPoint;
    public int maxStandCount = 10;

    public Plate platePrefebs;
    public AddItem plateAddPrefebs;
    private AddItem addPlate;
    public GameObject plateParent;
    private int plateCount;
    public int maxPlateCount = 12;

    private void Start()
    {
        baseMaxRowCount = 5;
        baseMaxColCount = 5;
        furnaceMaxRowCount = 2;
        furnaceMaxColCount = 5;
        furnaceCount = 1;
        maxStandCount = 10;
        maxPlateCount = 12;

        SetBase();
        SetFurnace();
        SetStand();
        SetPlate();
    }
    private void SetBase()
    {
        nextBasePos = createItem.itemBases[0].transform.position;
        nextBasePos.x += 50f;
        addBase = Instantiate(baseAddPrefebs, nextBasePos, Quaternion.identity);
        addBase.transform.SetParent(createItem.itemBases[0].transform.parent);
        addBase.transform.localScale = Vector3.one;
        addBase.ExpendMgr = this;
    }

    public void AddBase()
    {
        ItemBase itemBase = Instantiate(basePrefebs, addBase.transform.position, Quaternion.identity);
        createItem.itemBases.Add(itemBase);
        itemBase.item.itemMgr = createItem;
        itemBase.transform.SetParent(createItem.itemBases[0].transform.parent);
        itemBase.transform.localScale = Vector3.one;
        if (createItem.itemBases.Count >= baseMaxColCount * baseMaxRowCount )
        {
            Destroy(addBase.gameObject);
            return;
        }
            

        if (createItem.itemBases.Count % 5 == 0)
        {
            nextBasePos.x = createItem.itemBases[0].transform.position.x;
            nextBasePos.y += 50f;
        }
        else
        {
            nextBasePos.x += 50f;
        }

        addBase.transform.position = nextBasePos;
        addBase.SetNeedGold(addBase.increseGold);

    }

    private void SetFurnace()
    {
        firstFurnace.repository = sellAreaMgr.repository;
        Vector2 timeCirclePos = firstFurnace.transform.position;
        timeCirclePos.y += 10f;
        firstFurnace.bakeTimeCircle = Instantiate(bakeTimeCircle, timeCirclePos, Quaternion.identity);
        firstFurnace.bakeTimeCircle.transform.localScale = Vector3.one;
        firstFurnace.bakeTimeCircle.transform.SetParent(playUi.transform);

        nextFurnacePos = firstFurnace.transform.position;
        nextFurnacePos.x += 50f;
        addFurnace = Instantiate(furnaceAddPrefebs, nextFurnacePos, Quaternion.identity);
        addFurnace.transform.SetParent(firstFurnace.transform.parent);
        addFurnace.transform.localScale = Vector3.one;
        addFurnace.ExpendMgr = this;
    }

    public void AddFurnace()
    {
        Furnace furnace = Instantiate(furnacePrefebs, addFurnace.transform.position, Quaternion.identity);
        furnace.repository = sellAreaMgr.repository;
        furnace.transform.SetParent(firstFurnace.transform.parent);
        furnace.transform.localScale = Vector3.one;

        Vector2 timeCirclePos = furnace.transform.position;
        timeCirclePos.y += 10f;
        furnace.bakeTimeCircle = Instantiate(bakeTimeCircle, timeCirclePos, Quaternion.identity);
        furnace.bakeTimeCircle.transform.localScale = Vector3.one;
        furnace.bakeTimeCircle.transform.SetParent(playUi.transform);
        furnace.bakeTimeCircle.defColor = new Color(0.7f, 1f, 0.7f);
        furnaceCount++;

        if (furnaceCount >= furnaceMaxColCount * furnaceMaxRowCount)
        {
            Destroy(addFurnace.gameObject);
            return;
        }


        if (furnaceCount % 5 == 0)
        {
            nextFurnacePos.x = firstFurnace.transform.position.x;
            nextFurnacePos.y -= 50f;
        }
        else
        {
            nextFurnacePos.x += 50f;
        }

        addFurnace.transform.position = nextFurnacePos;
        addFurnace.SetNeedGold(addFurnace.increseGold);
    }

    private void SetStand()
    {
        nextStandPos = firstStand.transform.position;
        nextStandPos.x -= mag;
        addStand = Instantiate(standAddPrefebs, nextStandPos, Quaternion.identity);
        addStand.transform.SetParent(firstStand.transform.parent);
        addStand.transform.localScale = Vector3.one;
        addStand.ExpendMgr = this;

        addWayPoint = new GameObject("wayPoint");
        addWayPoint.transform.SetParent(wayPointList.transform);
        addWayPoint.transform.position = new Vector2(addStand.transform.position.x, addStand.transform.position.y - mag);

        standMgr.addStandWayPoint = addWayPoint.transform;
    }

    public void AddStand()
    {
        Stand stand = Instantiate(standPrefebs, addStand.transform.position, Quaternion.identity);
        standMgr.stands.Add(stand);
        stand.gameManager = gm;
        stand.repository = sellAreaMgr.repository;
        stand.standMgr = standMgr;
        stand.transform.SetParent(standMgr.transform);
        stand.transform.localScale = Vector3.one;
        if (createItem.itemBases.Count >= maxStandCount)
        {
            Destroy(addBase.gameObject);
            return;
        }
        if (standMgr.stands.Count % 5 == 0)
        {
            if ((standMgr.stands.Count / 5) % 2 != 0)
            {
                addStand.transform.position =
                    new Vector2(addStand.transform.position.x - mag, addStand.transform.position.y + mag * 2);
            }
            else
            {
                addStand.transform.position =
                                    new Vector2(addStand.transform.position.x + mag, addStand.transform.position.y + mag * 2);
            }
        }
        else
        {
            if ((standMgr.stands.Count / 5) % 2 != 0)
            {
                addStand.transform.position =
                    new Vector2(addStand.transform.position.x + mag, addStand.transform.position.y);
            }
            else
            {
                addStand.transform.position =
                    new Vector2(addStand.transform.position.x - mag, addStand.transform.position.y);
            }
        }
        addStand.transform.SetAsLastSibling();
        addStand.SetNeedGold(addStand.increseGold);

        addWayPoint.transform.position = new Vector2(addStand.transform.position.x, addStand.transform.position.y - mag);
        standMgr.SetWayPoint();

    }

    public void SetPlate()
    {
        Plate plate = Instantiate(platePrefebs, plateParent.transform);
        plate.transform.localScale = Vector3.one;
        plateCount = 1;

        sellAreaMgr.repository.SetPlateList(plate);

        addPlate = Instantiate(plateAddPrefebs, plateParent.transform);
        addPlate.transform.localScale = Vector3.one;
        addPlate.ExpendMgr = this;


    }

    public void AddPlate()
    {
        Plate plate = Instantiate(platePrefebs, plateParent.transform);
        plate.transform.localScale = Vector3.one;
        plateCount++;

        sellAreaMgr.repository.SetPlateList(plate);

        addPlate.transform.SetAsLastSibling();

        if(plateCount >= maxPlateCount)
            Destroy(addPlate.gameObject);
    }
}

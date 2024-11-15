enum item_type_e
{
    Bag,
    Weapon,
    Helmet,
    Pickaxe
};

struct BagItemData
{
    int MaterialType;
};

BagItemData bag_db[20];
ToolItemData tool_db[20];

struct Item
{
    item_type_e type;
    char name[32];
    int data_index;
};

void empty_bag(Item i )
{
    if (i != ITEM_BAG)
    {
        return;
    }

    
}

void use_item(Item i)
{
    switch(i.type)
    {
    case Pickaxe:
        {
        //anything goes  OOP = 
    }
}





using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ItemsDataTestScript
{
    
    public ItemsDatabaseTest itemDatabase;

    // Loads a test database for the purpose of this test
    [SetUp]
    public void SetUp()
    {
        itemDatabase = Resources.Load<ItemsDatabaseTest>("ItemsDatabaseTest");
        if (itemDatabase == null) 
        {
            Debug.Log("Database asset not found");
        }  
    }

    [Test]
    public void ShouldReturnItemDescription()
    {
        // To test that the getItemDataByName method works, we'll see if the correct description is fetched from the item name.
        string goalItem = "Sword";
        string expectedDescription = "This is a sword.";

        ItemsDatabaseTest.ItemData itemData = itemDatabase.GetItemDataByName(goalItem);
        string itemDescription = itemData.description;

        Assert.AreEqual(expectedDescription, itemDescription, "Should return the correct Item Description.");   
    }
}

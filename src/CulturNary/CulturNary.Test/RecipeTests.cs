using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using CulturNary.Web.Data;
using CulturNary.Web.Models;
using System.Diagnostics;
using System.Net;

namespace CulturNary_Tests;

/**
 * This is the recommended way to test using the in-memory db.  Seed the db and then write your tests based only on the seed
 * data + anything else you do to it.  No other tests will modify the db for that test.  Every test gets a brand new seeded db.
 * 
 */
public class Recipe_Tests
{
    private static readonly string _seedFile = @"Data/seedTest.sql";

    // Create this helper like this, for whatever context you desire
    private InMemoryDbHelper<CulturNaryDbContext> _dbHelper = new InMemoryDbHelper<CulturNaryDbContext>(_seedFile, DbPersistence.OneDbPerTest);

    /*
    [Test]
    public void SimpleContext_HasBeenSeeded()
    {
        using CulturNaryDbContext context = _dbHelper.GetContext();

        // Arrange
        //used seedTest to seed the in mem DB

        // Act
       
        // Assert
        Assert.That(context.Recipes.Count(), Is.EqualTo(5));
    }
    */
   
}
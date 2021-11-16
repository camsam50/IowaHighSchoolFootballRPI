using Microsoft.Azure.Cosmos;
using Models.Data.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CosmosDB
{
    public class CosmosPOC
    {


        // ADD THIS PART TO YOUR CODE

        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string EndpointUri = "https://iowahighschoolfootballrpicosmosdb-nonprod.documents.azure.com:443/";
        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "MfeEWdkcBx1djaUY6J58eUxNgUonfVs2eMcH0vwn68tLfTnvfhRsfEvOUKQSah2EP5YEMGRP3d5v78iI4AWUjQ==";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private static string databaseId = "IowaHighSchoolFootballRPI";
        private static string containerId = "Teams";



        public static async Task Testing()
        {

            var client = new CosmosClient(EndpointUri, PrimaryKey);
            var database = client.GetDatabase(databaseId);
            var container = database.GetContainer(containerId); //TODO: delete and recreate container to mimic "truncate"

            var t = new Team()
            {
                LongName = "EYE EM GEE",
                ShortName = "IMG",
                Class = "6A",
                District = 1
            };

            
            
            
            
            ItemResponse<Team> teamResponse = await container.CreateItemAsync<Team>(t);
            Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", teamResponse.Resource.Id, teamResponse.RequestCharge);



            //ItemResponse<Team> andersenFamilyResponse = await container.ReadItemAsync<Team>(t.Id, new PartitionKey(t.Class));
            //Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);



            var sqlQueryText = "SELECT * FROM c WHERE c.Class = '6A'";

            Console.WriteLine("Running query: {0}\n", sqlQueryText);

            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Team> queryResultSetIterator = container.GetItemQueryIterator<Team>(queryDefinition);

            List<Team> teams = new List<Team>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Team> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Team teem in currentResultSet)
                {
                    teams.Add(teem);
                    Console.WriteLine("\tRead {0}\n", teem);
                }
            }

            foreach(Team tm in teams)
            {
                Console.Write(tm.Id);
            }




            ItemResponse<Team> updateResponse = await container.ReadItemAsync<Team>("EYE EM GEE", new PartitionKey("6A"));
            var itemBody = updateResponse.Resource;

            itemBody.District = 2;

            // replace the item with the updated content
            updateResponse = await container.ReplaceItemAsync<Team>(itemBody, itemBody.Id, new PartitionKey(itemBody.Class));
            Console.WriteLine("Updated Team [{0},{1}].\n \tBody is now: {2}\n", itemBody.LongName, itemBody.Id, updateResponse.Resource);




            // Delete an item. Note we must provide the partition key value and id of the item to delete
            ItemResponse<Team> wakefieldFamilyResponse = await container.DeleteItemAsync<Team>(t.Id, new PartitionKey(t.Class));
            Console.WriteLine("Deleted Team [{0},{1}]\n", t.Class, t.Id);


        }


        //public static async Task Main(string[] args)
        //{
        //    try
        //    {
        //        Console.WriteLine("Beginning operations...\n");
        //        CosmosPOC p = new CosmosPOC();
        //        await p.GetStartedDemoAsync();

        //    }
        //    catch (CosmosException de)
        //    {
        //        Exception baseException = de.GetBaseException();
        //        Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error: {0}", e);
        //    }
        //    finally
        //    {
        //        Console.WriteLine("End of demo, press any key to exit.");
        //        Console.ReadKey();
        //    }
        //}

        

        //// ADD THIS PART TO YOUR CODE
        ///*
        //    Entry point to call methods that operate on Azure Cosmos DB resources in this sample
        //*/
        //public async Task GetStartedDemoAsync()
        //{
        //    // Create a new instance of the Cosmos Client
        //    this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
        //    await this.CreateDatabaseAsync();
        //    await this.CreateContainerAsync();


        //}


        ///// <summary>
        ///// Create the database if it does not exist
        ///// </summary>
        //private async Task CreateDatabaseAsync()
        //{
        //    // Create a new database
        //    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        //    this.database = this.cosmosClient.GetDatabase(databaseId);


        //    Console.WriteLine("Created Database: {0}\n", this.database.Id);
        //}

        ///// <summary>
        ///// Create the container if it does not exist. 
        ///// Specifiy "/LastName" as the partition key since we're storing family information, to ensure good distribution of requests and storage.
        ///// </summary>
        ///// <returns></returns>
        //private async Task CreateContainerAsync()
        //{
        //    // Create a new container
        //    this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/Class");
        //    this.container = this.database.GetContainer(containerId);

        //    //var adsfads = await this.database.(containerId);

        //    Console.WriteLine("Created Container: {0}\n", this.container.Id);
        //}


        ///// <summary>
        ///// Add Family items to the container
        ///// </summary>
        //private async Task AddItemsToContainerAsync()
        //{
        //    // Create a family object for the Andersen family
        //    Family andersenFamily = new Family
        //    {
        //        Id = "Andersen.1",
        //        LastName = "Andersen",
        //        Parents = new Parent[]
        //        {
        //    new Parent { FirstName = "Thomas" },
        //    new Parent { FirstName = "Mary Kay" }
        //        },
        //        Children = new Child[]
        //        {
        //    new Child
        //    {
        //        FirstName = "Henriette Thaulow",
        //        Gender = "female",
        //        Grade = 5,
        //        Pets = new Pet[]
        //        {
        //            new Pet { GivenName = "Fluffy" }
        //        }
        //    }
        //        },
        //        Address = new Address { State = "WA", County = "King", City = "Seattle" },
        //        IsRegistered = false
        //    };

        //    try
        //    {

        //        var t = new Team();
        //        ItemResponse<Team> teamResponse = await this.container.ReadItemAsync<Team>(t.Id, new PartitionKey(t.Class));

                
        //        // Read the item to see if it exists.  
        //        ItemResponse<Family> andersenFamilyResponse = await this.container.ReadItemAsync<Family>(andersenFamily.Id, new PartitionKey(andersenFamily.LastName));
        //        Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
        //    }
        //    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        //    {
        //        // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
        //        ItemResponse<Family> andersenFamilyResponse = await this.container.CreateItemAsync<Family>(andersenFamily, new PartitionKey(andersenFamily.LastName));

        //        // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
        //        Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
        //    }

        //    // Create a family object for the Wakefield family
        //    Family wakefieldFamily = new Family
        //    {
        //        Id = "Wakefield.7",
        //        LastName = "Wakefield",
        //        Parents = new Parent[]
        //        {
        //    new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
        //    new Parent { FamilyName = "Miller", FirstName = "Ben" }
        //        },
        //        Children = new Child[]
        //        {
        //    new Child
        //    {
        //        FamilyName = "Merriam",
        //        FirstName = "Jesse",
        //        Gender = "female",
        //        Grade = 8,
        //        Pets = new Pet[]
        //        {
        //            new Pet { GivenName = "Goofy" },
        //            new Pet { GivenName = "Shadow" }
        //        }
        //    },
        //    new Child
        //    {
        //        FamilyName = "Miller",
        //        FirstName = "Lisa",
        //        Gender = "female",
        //        Grade = 1
        //    }
        //        },
        //        Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
        //        IsRegistered = true
        //    };

        //    try
        //    {
        //        // Read the item to see if it exists
        //        ItemResponse<Family> wakefieldFamilyResponse = await this.container.ReadItemAsync<Family>(wakefieldFamily.Id, new PartitionKey(wakefieldFamily.LastName));
        //        Console.WriteLine("Item in database with id: {0} already exists\n", wakefieldFamilyResponse.Resource.Id);
        //    }
        //    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        //    {
        //        // Create an item in the container representing the Wakefield family. Note we provide the value of the partition key for this item, which is "Wakefield"
        //        ItemResponse<Family> wakefieldFamilyResponse = await this.container.CreateItemAsync<Family>(wakefieldFamily, new PartitionKey(wakefieldFamily.LastName));

        //        // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
        //        Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", wakefieldFamilyResponse.Resource.Id, wakefieldFamilyResponse.RequestCharge);
        //    }
        //}




    }
}

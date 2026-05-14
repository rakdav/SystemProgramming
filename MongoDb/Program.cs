// See https://aka.ms/new-console-template for more information
using MongoDb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Reflection.Metadata;
using System.Text.Json;
MongoClient client = new MongoClient("mongodb://localhost:27017");
//using (var cursor = await client.ListDatabaseNamesAsync())
//{
//    var databases = cursor.ToList();
//    foreach (var database in databases)
//    {
//        Console.WriteLine(database);
//    }
//}
IMongoDatabase database = client.GetDatabase("test");
//var collections = await database.ListCollectionsAsync();
//foreach (var collection in collections.ToList())
//{
//    Console.WriteLine(collection);
//}
IMongoCollection<BsonDocument> users = database.GetCollection<BsonDocument>("persons");

//Person person = new Person { Name = "Tom", Age = 38 };
//person.Company = new Company { Name = "Microsoft" };
//Console.WriteLine(person.ToJson());

//BsonDocument doc = new BsonDocument
//{
//    {"Name","Tom"},
//    {"Age", 38},
//    {"Company", new BsonDocument{ {"Name" , "Microsoft"}} },
//    {"Languages", new BsonArray{"english", "german", "spanish"} }
//};
//Person person = BsonSerializer.Deserialize<Person>(doc);
//Console.WriteLine(person.ToJson());

//Person person1 = new Person
//{
//    Name = "Tom",
//    Age = 38,
//    Company = new Company { Name = "Microsoft" },
//    Languages = {"english","german","spanish"}
//};
//Person person2 = new Person
//{
//    Name = "Misha",
//    Age = 20,
//    Company = new Company { Name = "Samsung" },
//    Languages = { "evrit" }
//};
//BsonDocument doc1 = person1.ToBsonDocument();
//BsonDocument doc2 = person2.ToBsonDocument();

//await users.InsertManyAsync(new List<BsonDocument> { doc1, doc2 });
//await users.InsertOneAsync(doc);

//List<BsonDocument> persons = await users.Find(new BsonDocument()).ToListAsync();
//foreach (var user in persons)
//{
//    Console.WriteLine(user);
//}

var filter = new BsonDocument { { "Name", new BsonDocument("$ne", "Misha") } };
List<BsonDocument> MishaList = await users.Find(filter).ToListAsync();
foreach (var user in MishaList)
{
    Console.WriteLine(user);
}

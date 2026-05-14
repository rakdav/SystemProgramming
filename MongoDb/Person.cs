using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb
{
    public class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int Age {  get; set; }
        public List<string>? Languages { get; set; } = new();
        public string? Name {  get; set; }
        public Company? Company { get; set; }
    }
}

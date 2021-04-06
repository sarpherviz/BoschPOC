using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bosch.API.Model.Domain
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        [BsonElement("machineId")]
        public string MachineId { get; set; }

        [BsonDateTimeOptions]
        [BsonElement("receiveDate")]
        public DateTime ReceiveDate { get; set; }

        [BsonElement("productBarcode")]
        public string ProductBarcode { get; set; }
    }
}

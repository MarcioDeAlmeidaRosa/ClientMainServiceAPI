﻿using ClientMainServiceAPI.Model.Contracts;
using MongoDB.Driver;
using ClientMainServiceAPI.Domain;
using System;
using System.Collections.Generic;

namespace ClientMainServiceAPI.Model.DB
{
    public class DBFactory<T> : IDBFactory<T> where T : DomainBase
    {
        protected MongoClient _client = null;
        protected IMongoDatabase _db;
        private string _collectionName;

        public DBFactory(string dataBaseName, string collectionName)
        {
            var host = Environment.GetEnvironmentVariable("CNN-MONGO-DB-CLIENT-HOST-API");
            var port = Convert.ToInt32(Environment.GetEnvironmentVariable("CNN-MONGO-DB-CLIENT-PORT-API"));
            var user = Environment.GetEnvironmentVariable("CNN-MONGO-DB-CLIENT-USER-API");
            var password = Environment.GetEnvironmentVariable("CNN-MONGO-DB-CLIENT-PASSWORD-API");

            //http://mongodb.github.io/casbah/3.1/reference/connecting/
            var mongoCredential = MongoCredential.CreateCredential(dataBaseName, user, password);

            var mongoClientSettings = new MongoClientSettings
            {
                WaitQueueSize = int.MaxValue,
                WaitQueueTimeout = new TimeSpan(0, 2, 0),
                MinConnectionPoolSize = 1,
                MaxConnectionPoolSize = 25,
                Credentials = new[] { mongoCredential },
                Server = new MongoServerAddress(host, port)
            };

            _client = new MongoClient(mongoClientSettings);
            _db = _client.GetDatabase(dataBaseName);
            _collectionName = collectionName;
        }

        public T Create(T entity)
        {
            _db.GetCollection<T>(_collectionName).InsertOne(entity);
            return entity;
        }

        public T GetById(string id)
        {
            return _db.GetCollection<T>(_collectionName).Find(c => c.Id.Equals(id)).FirstOrDefault();
        }
    }
}
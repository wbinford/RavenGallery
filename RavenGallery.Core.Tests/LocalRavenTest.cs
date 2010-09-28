﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Raven.Client.Document;
using Raven.Database;
using NUnit.Framework;

namespace RavenGallery.Core.Tests
{
    public class LocalRavenTest
    {
        private string path;
        private DocumentStore store;
        public DocumentStore Store { get { return store; } }

        [SetUp]
        public void CreateStore()
        {
            path = Path.GetDirectoryName(Assembly.GetAssembly(typeof(LocalRavenTest)).CodeBase);
            path = Path.Combine(path, "TestDb").Substring(6);
            ClearPath();

            store = new DocumentStore
            {
                Configuration = new RavenConfiguration
                {
                    DataDirectory = path,
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true
                }

            };
            store.Initialize();
        }

        [TearDown]
        public void DestroyStore()
        {
            store.Dispose();
            ClearPath();
        }

        private void ClearPath()
        {

            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMoq;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Common.Mocks;
using CleanArchitecture.Domain.Products;
using NUnit.Framework;

namespace CleanArchitecture.Application.Products.Queries.GetProductsList
{
    [TestFixture]
    public class GetProductsListQueryTests
    {
        private GetProductsListQuery _query;
        private AutoMoqer _mocker;
        
        private const int Id = 1;
        private const string Name = "Product 1";

        [SetUp]
        public void SetUp()
        {
            _mocker = new AutoMoqer();

            var product = new Product()
            {
                Id = Id,
                Name = Name
            };

            var products = new List<Product>()
            {
                product
            };

            _mocker.GetMock<IRepository<Product>>()
                .Setup(p => p.GetAll())
                .Returns(products.AsQueryable());

            _mocker.GetMock<IDatabaseService>()
                .Setup(p => p.Products)
                .Returns(_mocker.GetMock<IRepository<Product>>().Object);


            _query = _mocker.Create<GetProductsListQuery>();
        }

        [Test]
        public void TestExecuteShouldReturnListOfProducts()
        {
            var results = _query.Execute();

            var result = results.Single();

            Assert.That(result.Id, Is.EqualTo(Id));
            Assert.That(result.Name, Is.EqualTo(Name));
        }
    }
}

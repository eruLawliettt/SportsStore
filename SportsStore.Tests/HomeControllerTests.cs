using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void CanUseRepository()
        {
            //Arrange
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new() { Id = 1, Name = "P1" },
                new() { Id = 2, Name = "P2" }
                }).AsQueryable());

            HomeController controller = new(mock.Object);

            //Act
            IEnumerable<Product>? result =
                (controller.Index() as ViewResult)?.ViewData.Model
                    as IEnumerable<Product>;

            //Assert
            Product[] products = result?.ToArray()
                ?? [];

            Assert.True(products.Length == 2);
            Assert.Equal("P1", products[0].Name);
            Assert.Equal("P2", products[1].Name);
        }

        [Fact]
        public void CanPaginate()
        {
            //Arrange
            Mock<IStoreRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new() {Id = 1, Name = "P1"},
                new() {Id = 2, Name = "P2"},
                new() {Id = 3, Name = "P3"},
                new() {Id = 4, Name = "P4"},
                new() {Id = 5, Name = "P5"}
            }).AsQueryable<Product>());

            HomeController controller = new(mock.Object)
            {
                PageSize = 3
            };

            //Act
            IEnumerable<Product> result =
                (controller.Index(2) as ViewResult)?.ViewData.Model
                    as IEnumerable<Product>
                        ?? [];

            //Assert
            Product[] products = result.ToArray();
            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }
    }
}

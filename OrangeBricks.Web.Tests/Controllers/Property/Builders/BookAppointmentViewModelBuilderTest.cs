using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OrangeBricks.Web.Models;
using Moq;
using OrangeBricks.Web.Controllers.Property.Builders;
using System.Collections.Generic;
using System.Data.Entity;
using NSubstitute;
using System.Linq;

namespace OrangeBricks.Web.Tests.Controllers.Property.Builders
{
    [TestFixture]
    public class BookAppointmentViewModelBuilderTest
    {
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {            
            _context = Substitute.For<IOrangeBricksContext>();            
        }

        [Test]
        [NUnit.Framework.ExpectedException]
        public void BuildInvalidIdTest()
        {
            //Arrange
            BookAppointmentViewModelBuilder builder = new BookAppointmentViewModelBuilder(_context);
            IQueryable<Models.Property> properties = new List<Models.Property>{
                new Models.Property{ Id=1, StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ Id=2, StreetName = "Jones Street", Description = "", IsListedForSale = true}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Models.Property>>();
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.Provider).Returns(properties.Provider);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.Expression).Returns(properties.Expression);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.ElementType).Returns(properties.ElementType);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.GetEnumerator()).Returns(properties.GetEnumerator());

            _context.Properties.Returns(mockDbSet.Object);

            //Act
            var viewModel = builder.Build(3);

            //Assert
            // Will throw an exception
        }

        [Test]
        public void BuildReturnsCorrectPropertyDetails()
        {
            //Arrange
            BookAppointmentViewModelBuilder builder = new BookAppointmentViewModelBuilder(_context);

            IQueryable<Models.Property> properties = new List<Models.Property>{
                new Models.Property{ Id=1, StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ Id=2, StreetName = "Jones Street", Description = "", IsListedForSale = true}
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Models.Property>>();
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.Provider).Returns(properties.Provider);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.Expression).Returns(properties.Expression);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.ElementType).Returns(properties.ElementType);
            mockDbSet.As<IQueryable<Models.Property>>().Setup(m => m.GetEnumerator()).Returns(properties.GetEnumerator());
            mockDbSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(x => properties.ToList().FirstOrDefault());

            _context.Properties.Returns(mockDbSet.Object);
            //Act
            var viewModel = builder.Build(1);

            //Assert
            NUnit.Framework.Assert.AreEqual(1, viewModel.PropertyId);
            NUnit.Framework.Assert.AreEqual("Smith Street", viewModel.StreetName);
        }

    }
}

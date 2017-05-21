using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PhoneBook.Model;

namespace PhoneBook
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestAddCorrectly()
        {
            var mockSet = new Mock<DbSet<Person>>();
            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            var model = new Model.Model(mockContext.Object);
            var person = new Person("Test");
            model.AddPerson(person);
            
            mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetListOfPersonsCorrectly()
        {
            var data = new List<Person>
            {
                new Person("Testov"),
                new Person("Testinyan")
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            var model = new Model.Model(mockContext.Object);
            var result = model.GetListOfPersons().ToList();
            Assert.AreEqual(2, result.Count);

            result = model.GetListOfPersons("Testov").ToList();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Testov", result[0].LastName);

            result = model.GetListOfPersons("äbracadabra").ToList();
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void UpdateTest()
        {
            var data = new List<Person>
            {
                new Person("Testov")
                {
                    Id = 0
                },
                new Person("Testinyan")
                {
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            var model = new Model.Model(mockContext.Object);

            model.UpdatePerson(0, new Person("Secondov"));

            var result = model.GetListOfPersons().ToList();
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Secondov", result[0].LastName);
        }

        [Test]
        public void AddingMockFromListTest()
        {
            var data = new List<Person>
            {
                new Person("Testov")
                {
                    Id = 0
                },
                new Person("Testinyan")
                {
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            var model = new Model.Model(mockContext.Object);

            var result = model.GetListOfPersons().ToList();
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void DeleteTest()
        {
            var data = new List<Person>
            {
                new Person("Testov")
                {
                    Id = 0
                },
                new Person("Testinyan")
                {
                    Id = 1
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Person>>();
            mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<PersonContext>();
            mockContext.Setup(m => m.Persons).Returns(mockSet.Object);

            var model = new Model.Model(mockContext.Object);
            model.DeletePerson(0);

            mockSet.Verify(m => m.Remove(It.IsAny<Person>()), Times.Once);
        }
    }
}
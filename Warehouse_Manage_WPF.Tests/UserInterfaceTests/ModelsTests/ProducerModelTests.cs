using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manage_WPF.UserInterface.Models;
using Xunit;

namespace Warehouse_Manage_WPF.Tests.UserInterfaceTests.ModelsTests
{
    public class ProducerModelTests
    {
        [Fact]
        public void CreateProjectModel()
        {
            // Arrange
            string Name = "FESTO";
            string URL = "https://festo.com/";

            // Act
            ProducerModel producer = new ProducerModel()
            {
                Name = Name,
                URL = URL
            };

            // Assert
            Assert.Equal(Name, producer.Name);
            Assert.Equal(URL, producer.URL);
        }

        [Fact]
        public void ConvertProducerModelToProducerEntity_CheckProperties()
        {
            // Arrange
            string Name = "FESTO";
            string URL = "https://festo.com/";

            ProducerModel producer = new ProducerModel()
            {
                Name = Name,
                URL = URL
            };

            // Act
            var producerEntity = producer.ConvertToProducerEntity();

            // Assert
            Assert.True(producerEntity.GetType() == typeof(DataAccess.Entities.Producer));
            Assert.Equal(Name, producerEntity.Name);
            Assert.Equal(URL, producerEntity.URL);
        }

        [Fact]
        public void ConvertProducerModelToProducerEntity_CheckDefaultValues()
        {
            // Arrange
            ProducerModel producer = new ProducerModel()
            {
                Name = "SomeName",
                URL = "SomeURL"
            };

            // Act
            var producerEntity = producer.ConvertToProducerEntity();

            // Assert
            Assert.True(producerEntity.GetType() == typeof(DataAccess.Entities.Producer));
            Assert.Equal(0, producerEntity.Id);
            Assert.Null(producerEntity.Devices);
        }
    }
}

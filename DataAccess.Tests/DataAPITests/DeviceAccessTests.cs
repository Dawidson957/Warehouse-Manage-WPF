using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DataAccess.EntityModel;
using DataAccess.Entities;
using DataAccess.DataAcc;
using System.Data.Entity;
using Moq;
using Xunit;

namespace DataAccess.Tests.DataAPITests
{
    public class DeviceAccessTests
    {
        [Fact]
        public void GetDevicesAll()
        {
            var data = new List<Device>
            {
                new Device
                {
                    Id = 2,
                    Name = "sampleName1",
                    ArticleNumber = "sampleArticleNumber1",
                    Location = "A3",
                    Quantity = 14,
                    Producer = new Producer
                    {
                        Name = "sampleProducerName1",
                        URL = "sampleURL1"
                    },
                    ProjectID = 3
                },
                new Device
                {
                    Id = 3,
                    Name = "sampleName2",
                    ArticleNumber = "sampleArticleNumber2",
                    Location = "B2",
                    Quantity = 8,
                    Producer = new Producer
                    {
                        Name = "sampleProducerName2",
                        URL = "sampleURL2"
                    },
                    ProjectID = 3
                },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Device>>();
            mockSet.As<IQueryable<Device>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Device>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Device>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Device>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<WarehouseModel>();
            mockContext.Setup(x => x.Devices).Returns(mockSet.Object);

            //var service = DeviceAccess(m)


        }
    }
}

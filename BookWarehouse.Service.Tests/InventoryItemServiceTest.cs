using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Infrastructure;
using BookWarehouse.Core.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookWarehouse.Service.Tests
{
    [TestClass]
    public class InventoryItemServiceTest
    {
        private InventoryItemService _sut;

        private Mock<IRepository<InventoryItem>> _items;
        private Mock<ILogger> _log;

        [TestInitialize]
        public void TestInitialize()
        {
            _items = new Mock<IRepository<InventoryItem>>();
            _log = new Mock<ILogger>();

            _sut = new InventoryItemService(_items.Object, _log.Object);
        }

        [TestMethod]
        public void Create_CallsItemRepositoryAdd_OnCreate()
        {
            //Assemble
            var item = new InventoryItem();

            //Act
            _sut.Create(item);

            //Assert
            _items.Verify(x => x.Add(It.IsAny<InventoryItem>()), Times.Once());
        }

        [TestMethod]
        public void Create_LogsIncrease_OnCreate()
        {
            //Assemble
            var item = new InventoryItem();

            //Act
            _sut.Create(item);

            //Assert
            _log.Verify(x => x.Log(It.IsAny<LogAction>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [TestMethod]
        public void Delete_CallsItemRepositoryRemove_OnDelete()
        {
            //Assemble
            var itemId = Guid.Parse("DCD2F59C-42CA-498A-A347-A7125897A5C0");

            _items.Setup(x => x.Find(It.IsAny<Guid>()))
                .Returns(new InventoryItem {InventoryItemId = itemId});

            _items.Setup(x => x.SearchForMany(It.IsAny<Expression<Func<InventoryItem, bool>>>()))
                .Returns(new List<InventoryItem> {new InventoryItem()}.AsQueryable());

            //Act
            _sut.Delete(itemId);

            //Assert
            _items.Verify(x => x.Remove(itemId), Times.Once());
        }

        [TestMethod]
        public void Delete_CallsLog_OnDelete()
        {
            //Assemble
            var itemId = Guid.Parse("DCD2F59C-42CA-498A-A347-A7125897A5C0");

            _items.Setup(x => x.Find(It.IsAny<Guid>()))
                .Returns(new InventoryItem { InventoryItemId = itemId });

            _items.Setup(x => x.SearchForMany(It.IsAny<Expression<Func<InventoryItem, bool>>>()))
                .Returns(new List<InventoryItem> { new InventoryItem() }.AsQueryable());

            //Act
            _sut.Delete(itemId);

            //Assert
            _log.Verify(x => x.Log(It.IsAny<LogAction>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [TestMethod]
        public void Update_CallsLogOnce_OnWarehouseChange()
        {
            //Assemble
            var itemId = Guid.Parse("66C2DD39-7AFA-4F07-BF03-98576AFBFA69");
            var newWarehouse = Guid.Parse("791BE20E-5FAA-44EE-B45C-4223DAB6987F");
            var oldWarehouse = Guid.Parse("1215D77A-6F14-4B02-BA9A-EDCF05248186");

            var oldItem = new InventoryItem { InventoryItemId = itemId, WarehouseId = oldWarehouse };
            var newItem = new InventoryItem { InventoryItemId = itemId, WarehouseId = newWarehouse };

            _items.Setup(x => x.Find(It.IsAny<Guid>())).Returns(oldItem);

            //Act
            _sut.Update(newItem);

            //Assert
            _log.Verify(x => x.Log(It.IsAny<LogAction>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [TestMethod]
        public void Update_CallsLog_OnPriceChange()
        {
            //Assemble
            var itemId = Guid.Parse("66C2DD39-7AFA-4F07-BF03-98576AFBFA69");
            var newPrice = 19.99m;
            var oldPrice = 24.99m;

            var oldItem = new InventoryItem { InventoryItemId = itemId, Price = oldPrice};
            var newItem = new InventoryItem { InventoryItemId = itemId, Price = newPrice };

            _items.Setup(x => x.Find(It.IsAny<Guid>())).Returns(oldItem);

            //Act
            _sut.Update(newItem);

            //Assert
            _log.Verify(x => x.Log(It.IsAny<LogAction>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Once());
        }

        [TestMethod]
        public void Update_CallsLogTwice_OnPriceAndWarehouseUpdate()
        {
            //Assemble
            var itemId = Guid.Parse("66C2DD39-7AFA-4F07-BF03-98576AFBFA69");
            var newWarehouse = Guid.Parse("791BE20E-5FAA-44EE-B45C-4223DAB6987F");
            var oldWarehouse = Guid.Parse("1215D77A-6F14-4B02-BA9A-EDCF05248186");
            var newPrice = 19.99m;
            var oldPrice = 24.99m;

            var oldItem = new InventoryItem { InventoryItemId = itemId, Price = oldPrice, WarehouseId = oldWarehouse };
            var newItem = new InventoryItem { InventoryItemId = itemId, Price = newPrice, WarehouseId = newWarehouse };

            _items.Setup(x => x.Find(It.IsAny<Guid>())).Returns(oldItem);

            //Act
            _sut.Update(newItem);

            //Assert
            _log.Verify(x => x.Log(It.IsAny<LogAction>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>()),
                Times.Exactly(2));
        }
    }
}

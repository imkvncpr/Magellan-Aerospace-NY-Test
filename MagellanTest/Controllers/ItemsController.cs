using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MagellanTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemDbContext _dbContext;

        public ItemsController(ItemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult CreateItem(ItemDto itemDto)
        {

            var newItem = new Item
            {
                ItemName = itemDto.ItemName,
                ParentItem = itemDto.ParentItem,
                Cost = itemDto.Cost,
                ReqDate = itemDto.ReqDate
            };

            ((List<Item>)_dbContext.Items).Add(newItem);
            _dbContext.SaveChanges();

            // Return the id of the newly created record
            return Ok(new { Id = newItem.Id });
        }

        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            // Query the item table by supplying the id of the record
             // Add this using directive

            var item = ((IEnumerable<Item>)_dbContext.Items).FirstOrDefault(i => (int)i.Id == id);

            // Return the item details in json
            var itemDto = new ItemDto
            {
                Id = item.Id,
                ItemName = item.ItemName,
                ParentItem = item.ParentItem,
                Cost = item.Cost,
                ReqDate = item.ReqDate
            };

            return Ok(itemDto);
        }

        private object IEnumerable<T>()
        {
            throw new NotImplementedException();
        }

        [HttpGet("total-cost")]
        public IActionResult GetTotalCost(string itemName)
        {
            // Call the Get_Total_Cost function with the supplied item_name
            var totalCost = Get_Total_Cost(itemName);

            // Return the value returned by the function
            return Ok(new { TotalCost = totalCost });
        }

        private decimal Get_Total_Cost(string itemName)
        {
           
            return 0; 
        }
    }

    internal class Item
    {
        public object ItemName { get; internal set; }
        public object ParentItem { get; internal set; }
        public object Cost { get; internal set; }
        public object ReqDate { get; internal set; }
        public object Id { get; internal set; }
    }

    public class ItemDto
    {
        public object ReqDate { get; internal set; }
        public object Cost { get; internal set; }
        public object ParentItem { get; internal set; }
        public object ItemName { get; internal set; }
        public object Id { get; internal set; }
    }

    public class ItemDbContext
    {
        public object Items { get; internal set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

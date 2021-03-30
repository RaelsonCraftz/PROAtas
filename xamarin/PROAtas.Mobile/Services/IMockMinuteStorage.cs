using PROAtas.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface IMockMinuteStorage<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(T item);
        Task<T> GetItemAsync(T item);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }

    public class MockMinuteStorage : IMockMinuteStorage<Minute>
    {
        readonly List<Minute> items;

        public MockMinuteStorage()
        {
            items = new List<Minute>()
            {
                new Minute { Id = Guid.NewGuid().ToString(), Name = "First item", Date = DateTime.Today.ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(1).ToString(Formats.TimeFormat), PeopleQuantity = 5 },
                new Minute { Id = Guid.NewGuid().ToString(), Name = "Second item", Date = DateTime.Today.AddDays(5).ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(3).ToString(Formats.TimeFormat), PeopleQuantity = 8 },
                new Minute { Id = Guid.NewGuid().ToString(), Name = "Third item", Date = DateTime.Today.AddDays(-12).ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(12).ToString(Formats.TimeFormat), PeopleQuantity = 12 },
                new Minute { Id = Guid.NewGuid().ToString(), Name = "Fourth item", Date = DateTime.Today.AddYears(-1).ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(-50).ToString(Formats.TimeFormat), PeopleQuantity = 55 },
                new Minute { Id = Guid.NewGuid().ToString(), Name = "Fifth item", Date = DateTime.Today.AddMonths(3).ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(22).ToString(Formats.TimeFormat), PeopleQuantity = 4 },
                new Minute { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Date = DateTime.Today.AddYears(1).ToString(Formats.DateFormat), Start = DateTime.Now.AddHours(-120).ToString(Formats.TimeFormat), PeopleQuantity = 32 }
            };
        }

        public async Task<bool> AddItemAsync(Minute item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Minute item)
        {
            var oldItem = items.Where((Minute e) => e.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Minute item)
        {
            var oldItem = items.Where((Minute e) => e.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Minute> GetItemAsync(Minute item)
        {
            return await Task.FromResult(items.FirstOrDefault(e => e.Id == item.Id));
        }

        public async Task<IEnumerable<Minute>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}

using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ShopBridgeApi.Models;
using ShopBridgeApi.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShopBridgeApi.Api.IntegrationTests.v1
{
    public class InventoryControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private readonly HttpClient _client;

        public InventoryControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        public static StringContent GetStringContent(object obj)
              => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");

        private static readonly Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [Fact]
        public async Task TestGetAllInventories()
        {
            var request = "/api/v1/inventory/get";

            var response = await _client.GetStringAsync(request);
            var result = JsonConvert.DeserializeObject<IEnumerable<Inventory>>(response);

            Assert.True(result.Any());
            Assert.True(result.ToList()[0].Name == "N95");
        }

        [Fact]
        public async Task TestGetInventoryByNameForExistingProduct()
        {
            var request = "/api/v1/inventory/get/N95";

            var response = await _client.GetStringAsync(request);
            var result = JsonConvert.DeserializeObject<Inventory>(response);

            Assert.True(result.Name == "N95");
        }
        [Fact]
        public async Task TestGetInventoryByNameShouldThrowNot0OundExceptionForNonExistingProduct()
        {
            var request = "/api/v1/inventory/get/Cell";
            var response = await _client.GetAsync(request);
            Assert.Equal(404, (int)response.StatusCode);
        }

        [Fact]
        public async Task TestAddInventory()
        {
            var request = new InventoryAddRequest
            {
                Name = RandomString(5),
                Description = RandomString(15),
                Price = 550,
                Quantity = 400,
                CreatedBy = "TestUser"
            };
            var content = GetStringContent(request);
            var response = await _client.PostAsync("/api/v1/inventory", content);
            Assert.Equal(200, (int)response.StatusCode);

            var inventory = await _client.GetStringAsync($"/api/v1/inventory/get/{request.Name}");
            var result = JsonConvert.DeserializeObject<Inventory>(inventory);

            Assert.True(result.Name == request.Name);
            Assert.True(result.Price == request.Price);
            Assert.True(result.Quantity == request.Quantity);
        }
        [Fact]
        public async Task TestAddInventoryShouldThrowBadRequestWhenRequestIsNull()
        {
            var content = GetStringContent(null);
            var response = await _client.PostAsync("/api/v1/inventory", content);
            Assert.Equal(400, (int)response.StatusCode);
        }

        [Fact]
        public async Task TestUpdateInventory()
        {
            var request = new InventoryAddRequest
            {
                Name = RandomString(5),
                Description = RandomString(15),
                Price = 30000,
                Quantity = 400,
                CreatedBy = "TestUser"
            };
            var content = GetStringContent(request);
            var response = await _client.PostAsync("/api/v1/inventory", content);
            Assert.Equal(200, (int)response.StatusCode);

            var inventory = await _client.GetStringAsync($"/api/v1/inventory/get/{request.Name}");
            var result = JsonConvert.DeserializeObject<Inventory>(inventory);

            var requestForUpdate = new InventoryUpdateRequest
            {
                Id = result.Id,
                Name = RandomString(5),
                Description = RandomString(15),
                Price = 35000,
                Quantity = 200,
                ModifiedBy = "TestUser"
            };
            var updateRequest = await _client.PutAsync("/api/v1/inventory", GetStringContent(requestForUpdate));
            Assert.Equal(200, (int)updateRequest.StatusCode);

            var updatedResponse = JsonConvert.DeserializeObject<Inventory>(await _client.GetStringAsync($"/api/v1/inventory/get/{requestForUpdate.Name}"));

            Assert.True(updatedResponse.Name != request.Name);
            Assert.True(updatedResponse.Price != request.Price);
            Assert.True(updatedResponse.Quantity != request.Quantity);
        }
        [Fact]
        public async Task TestUpdateInventoryShouldThrowNotFoundWhenEntityNotExists()
        {
            var requestForUpdate = new InventoryUpdateRequest
            {
                Id = 9999999,
                Name = RandomString(5),
                Description = RandomString(15),
                Price = 35000,
                Quantity = 200,
                ModifiedBy = "TestUser"
            };
            var updateRequest = await _client.PutAsync("/api/v1/inventory", GetStringContent(requestForUpdate));
            Assert.Equal(404, (int)updateRequest.StatusCode);
        }
        [Fact]
        public async Task TestUpdateInventoryShouldThrowBadRequestWhenRequestIsNull()
        {
            var updateRequest = await _client.PutAsync("/api/v1/inventory", GetStringContent(null));
            Assert.Equal(400, (int)updateRequest.StatusCode);
        }

        [Fact]
        public async Task TestDeleteInventory()
        {
            var request = new InventoryAddRequest
            {
                Name = RandomString(5),
                Description = RandomString(15),
                Price = 130000,
                Quantity = 100,
                CreatedBy = "TestUser"
            };
            var content = GetStringContent(request);
            var response = await _client.PostAsync("/api/v1/inventory", content);
            Assert.Equal(200, (int)response.StatusCode);

            var addedInventory = await _client.GetStringAsync($"/api/v1/inventory/get/{request.Name}");
            var addResult = JsonConvert.DeserializeObject<Inventory>(addedInventory);

            var deleteRequest = await _client.DeleteAsync($"/api/v1/inventory?id={addResult.Id}");
            Assert.Equal(200, (int)deleteRequest.StatusCode);

            var result = await _client.GetAsync($"/api/v1/inventory/get/{request.Name}");
            Assert.Equal(404, (int)result.StatusCode);
        }
        [Fact]
        public async Task TestDeleteInventoryReturnsBadRequestWhenIdIsNonZero()
        {
            var deleteRequest = await _client.DeleteAsync($"/api/v1/inventory?id=-99");
            Assert.Equal(400, (int)deleteRequest.StatusCode);
        }
        [Fact]
        public async Task TestDeleteInventoryReturnsNotFoundWhenRequestedEntityDoesNotExists()
        {
            var deleteRequest = await _client.DeleteAsync($"/api/v1/inventory?id=99999");
            Assert.Equal(404, (int)deleteRequest.StatusCode);
        }
    }
}
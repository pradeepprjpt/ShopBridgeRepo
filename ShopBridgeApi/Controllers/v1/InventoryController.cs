using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopBridgeApi.DAO;
using ShopBridgeApi.Models;
using ShopBridgeApi.Request;
using System;
using System.Threading.Tasks;

namespace ShopBridgeApi.Controllers.v1
{
    /// <summary>
    /// Inventory Controller
    /// </summary>
    [Route("api/v1/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="inventoryRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public InventoryController(IInventoryRepository inventoryRepository, IMapper mapper, ILogger<InventoryController> logger)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all inventories
        /// </summary>
        /// <returns>The list of inventories</returns>
        [HttpGet("get")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _inventoryRepository.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetAll:{ex.Message};{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Add inventory
        /// </summary>
        /// <param name="request">InventoryAddRequest Object</param>
        /// <returns>Returns created inventory</returns>
        [HttpPost]
        public async Task<IActionResult> Add(InventoryAddRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                var inventory = _mapper.Map<Inventory>(request);
                await _inventoryRepository.Add(inventory);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Add:{ex.Message};{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Deletes inventory by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();

                var result = await _inventoryRepository.Get(id);
                if (result == null)
                    return NotFound();

                await _inventoryRepository.Delete(result);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Delete:{ex.Message};{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Returns inventory by name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Inventory</returns>
        [HttpGet]
        [Route("get/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                var result = await _inventoryRepository.Get(name);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get:{ex.Message};{ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// Update inventory
        /// </summary>
        /// <param name="request">inventory</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(InventoryUpdateRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest();

                var result = await _inventoryRepository.Get(request.Id);
                if (result == null)
                    return NotFound();

                var inventory = _mapper.Map<Inventory>(request);
                await _inventoryRepository.Update(inventory);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Update:{ex.Message};{ex.StackTrace}");
                throw;
            }
        }
    }
}

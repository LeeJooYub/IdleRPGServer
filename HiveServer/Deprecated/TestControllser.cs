// using System;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;

// using HiveServer.Model.Entity;
// using HiveServer.Repository.Interfaces;
// using Microsoft.Extensions.Logging;
// using ZLogger;

// [ApiController]
// [Route("[controller]")]
// public class TestController : ControllerBase
// {
//     private readonly IHiveDb _hiveDb;
//     private readonly ILogger<TestController> _logger;

//     public TestController(IHiveDb hiveDb, ILogger<TestController> logger)
//     {
//         _hiveDb = hiveDb;
//         _logger = logger;
//     }

//     [HttpPost("AddSample")]
//     public async Task<IActionResult> AddSampleCategory()
//     {
//         var category = new Category
//         {
//             Slug = "sample-slug",
//             Title = "샘플 카테고리",
//             Description = "설명",
//             Image = "sample.png",
//             SortIds = 1,
//             Display = 1,
//             DeletedAt = null
//         };

//         var result = await _hiveDb.InsertCategoryAsync(category);
//         _logger.ZLogDebug($"[AddSampleCategory] Inserted Category with Id: {result}");
//         return Ok(new { Inserted = result });
//     }

//     [HttpPost("FindSample")]
//     public async Task<IActionResult> FindSampleCategory()
//     {
//         var result2 = await _hiveDb.GetCategoryByIdNoAliasAsync(2);
//         _logger.ZLogDebug($"[FindSampleCategory] Retrieved Category: {result2}");

//         return Ok(new { Retrieved = result2 });
//     }
// }
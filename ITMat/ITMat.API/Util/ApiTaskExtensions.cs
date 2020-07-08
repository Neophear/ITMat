using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.API.Util
{
    public static class ApiTaskExtensions
    {
        public static async Task<IActionResult> TryOrError<T>(Func<Task<T>> task)
        {
            try
            {
                var result = await task();

                return result as IActionResult ?? new OkObjectResult(result);
            }
            catch (KeyNotFoundException e) { return new NotFoundObjectResult(new { e.Message }); }
        }
    }
}
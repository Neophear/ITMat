using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.API.Util
{
    public static class ApiTaskExtensions
    {
        public static async Task<IActionResult> TryOrError<T>(ILogger logger, Func<Task<T>> task)
        {
            try
            {
                var result = await task();

                return result as IActionResult ?? new OkObjectResult(result);
            }
            catch (KeyNotFoundException e) { return new NotFoundObjectResult(new { e.Message }); }
            catch (Exception e)
            {
                logger.LogError(e, "An exception has occured: {exception}.");
                throw;
            }
        }
    }
}
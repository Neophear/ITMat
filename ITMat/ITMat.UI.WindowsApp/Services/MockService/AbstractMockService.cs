using System;
using System.Threading.Tasks;

namespace ITMat.UI.WindowsApp.Services.MockService
{
    public class AbstractMockService
    {
        protected async Task<T> ExecuteWithDelay<T>(Func<T> func)
        {
            await Task.Delay(200);
            return func();
        }

        protected async Task ExecuteWithDelay(Action action)
        {
            await Task.Delay(200);
            action();
        }
    }
}
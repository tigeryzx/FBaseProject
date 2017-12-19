using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFoxtec.WPF.Common.UIThread
{
    public class ThreadHelper
    {
        public static void StartTaskAndCallbackUI<T>(Func<T> fun,Action<Task<T>> action)
        {
            Task.Factory
            .StartNew(fun)
            .ContinueWith((task) =>
            {
                if (task.IsFaulted)
                    Console.WriteLine(task.Exception.GetBaseException());
                action(task);
            }, TaskScheduler.FromCurrentSynchronizationContext())
            .ContinueWith((task)=> {
                if (task.IsFaulted)
                    Console.WriteLine(task.Exception.GetBaseException());
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

    }
}

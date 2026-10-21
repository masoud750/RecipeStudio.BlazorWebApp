using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Common.Interfaces
{
    public interface IAppLogger
    {
        void LogInfo(string message);
        void LogError(Exception ex, string message);
        void LogWarning(Exception ex, string message);
       
        void LogFatal(Exception ex, string message);
    }
}

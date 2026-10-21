using RecipeStudio.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Application.Tests.Mocks
{
    internal class LoggerMock : IAppLogger
    {
        public void LogError(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void LogFatal(Exception ex, string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(Exception ex, string message)
        {
            throw new NotImplementedException();
        }
    }
}

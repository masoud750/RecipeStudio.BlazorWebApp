using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeStudio.Shared.Enums
{
    public enum OperationResult
    {
        Success,
        AlreadyExists,
        NotFound,
        ValidationFailed,
        Unauthorized,
        Error
    }
}

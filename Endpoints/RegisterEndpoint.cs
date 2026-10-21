using RecipeStudio.Application.Interfaces;
using RecipeStudio.Shared.DTOs;
using RecipeStudio.Application.Common.Interfaces;
namespace RecipeStudioBlazorApp.Server.Endpoints
{
    public static class RegisterEndpoint
    {

        public static void MapRegisterEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/register",
                async (
                         RegisterRequest registerRequest,
                         IUserRegisterService userRegisterService,
                         IAppLogger logger
                      ) =>
                          {
                              try
                              {
                                  var result = await userRegisterService.RegisterUserAsync(
                                   registerRequest.UserName, registerRequest.Password, registerRequest.Email);
                                  if (result)
                                  {
                                      var response = new RegisterResponse(result, "successfully added", Errors: Array.Empty<string>());
                                      logger.LogInfo("the user was registered.");
                                      return Results.Ok(response);
                                  }

                                  // registration failed 
                                  var failResponse = new RegisterResponse(result, "registration failed",
                                      Errors: new[] { "Unable to create user." });
                                  return Results.BadRequest(failResponse);
                              }
                              catch (Exception ex)
                              {
                                  Exception current = ex;
                                  string msg = string.Empty;

                                  while (current != null)
                                  {
                                      msg += $"< {current.GetType().Name}: {current.Message} >";
                                      current = current.InnerException;
                                  }

                                  // registration failed 
                                  var failResponse = new RegisterResponse(false,
                                      "registration failed", Errors: new[] { msg });
                                  logger.LogError(ex,"Failed to register the user.");
                                  return Results.BadRequest(failResponse);
                              }
                          });
        }
    }
}


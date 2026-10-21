This project follows the Clean Architecture principles described by Microsoft:
https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

When reviewing code, always verify that the architecture rules are respected:

- Dependencies must point inward toward the Application Core.
- Domain and Application layers must not depend on Infrastructure or UI layers.
- Infrastructure implementations must depend on abstractions defined in the Application Core.
- UI components must communicate through Application layer interfaces and should not directly access Infrastructure implementations.
- Business logic must remain inside the Domain/Application layers.
- Changes should follow the Separation of Concerns principle.

Additionally, review the code against SOLID principles:
- Single Responsibility Principle (SRP)
- Open/Closed Principle (OCP)
- Liskov Substitution Principle (LSP)
- Interface Segregation Principle (ISP)
- Dependency Inversion Principle (DIP)

Whenever you detect a Clean Architecture or SOLID violation:
1. Explain the violation.
2. Describe why it is problematic.
3. Suggest a refactoring approach.
4. Provide an improved code example when appropriate.
5. Indicate the severity (Low, Medium, High).

During code reviews, prioritize architectural correctness, maintainability, testability, and adherence to Clean Architecture and SOLID principles.
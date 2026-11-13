# Authentication in the Blazor interface

The Blazor Server interface sends login and logout requests to the web host, which calls the existing API endpoints (`/auth/login`, `/auth/me`, and `/auth/logout`). Set `Api:BaseUrl` to the API's HTTPS origin for each deployment. The development profile uses `https://localhost:7296/`.

The API bearer token is kept in the web host's in-memory authentication ticket store. The browser receives only an opaque, `HttpOnly`, `Secure`, `SameSite=Lax` cookie; the token is not written to browser storage or exposed to JavaScript. The cookie and server-side ticket expire at the API session's expiration time, currently eight hours. Logout revokes the API session and removes the local ticket.

The in-memory ticket store is suitable for the current single-instance host. A multi-instance or restart-resilient deployment must provide a shared distributed ticket store before running multiple web instances; configure a stable shared ASP.NET Core Data Protection key ring as well.

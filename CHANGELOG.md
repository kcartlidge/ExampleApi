# CHANGELOG

- 2022-10-07
  - Initial commit
  - Standard repo files added
  - Remove default controller and tidy slightly
  - Add sample (static) data source
  - Add generated API controller with read action
  - Add extra endpoints to simulate error states
  - Switch endpoint return types to `ActionResult`
    - In case you missed it, that's *not* `IActionResult`
    - The interface version won't allow BadRequest etc
    - `ActionResult` allows both data and BadRequest
      - Strongly-typed responses with support for other status codes
  - Add consumes/produces Swagger annotations to the controller

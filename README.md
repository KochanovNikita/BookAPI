# Create Book rest api.
1. API have 4 models: Book, Author Publisher, Pseudonim.
2. For each model you need to implement CRUD.
3. Using standard methods and a single interface.
  * GET: Retrieves a resource or collection of resources.
  * POST: Creates a new resource or sends data for processing.
  * PUT: Completely updates an existing resource, replacing it with new data.
  * PATCH: Partially updates a resource with specific changes.
  * DELETE: Deletes a resource.
4. Return accurate and informative HTTP status codes.
  * 2xx – Success: The request was successfully received, understood and accepted.
  * 3xx - Redirect: The request must take further action to complete the request.
  * 4xx - Client Error: The request has incorrect syntax or cannot be completed.
  * 5xx - Server Error: The server was unable to complete a seemingly valid request.
5. Cacheability*(Optional task with increased difficulty). REST APIs should provide response caching to improve performance.
 By caching response data, clients can reduce the latency of subsequent requests, minimize the load on servers, and reduce network traffic. To support caching:
  * Include cache-related HTTP headers such as Cache-Control, Expires, and ETag in API responses.
  * Ensure that resources have a unique and consistent URL, reducing the likelihood of duplicate entries in the client cache.

# Model fields.
1. Book
  * Id
  * Title (string)
  * Description (string)
  * DateCreated (date)
  * AuthorId
  * PublisherId

2. Author
  * Id
  * Name  (string)
  * Birthday (date)
  * DieDate  (date)
3. Pseudonym
  * Id
  * Name (string)
  * AuthorId
4. Publisher
  * Id
  * Name (string)
     

using System.Text;

namespace Library_.Middleware
{
    public class HttpLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public HttpLoggingMiddleware(RequestDelegate next, ILogger<HttpLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            var bodyStr = "";
            var req = context.Request;
            req.EnableBuffering();

            using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync();
                Console.WriteLine(bodyStr);
            }
                req.Body.Position = 0;

            _logger.LogInformation(GetRequestAsTextAsync(context.Request).Result);

            await _next(context);
        }

        private async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            string query = "";
            foreach (var item in request.Query.Keys)
            {
                query += "Key: " + item.ToString() + " Value: " + request.Query[item].ToString() + "\n";
            }
            string headers = "";
            foreach (var item in request.Headers.Keys)
            {
                headers += "\n" + item.ToString() + " : " + request.Headers[item].ToString();
            }

            return $"{"\n"}Headers: {headers}{"\n"}BODY: {bodyAsText} \nMethod: {request.Method} {"\n"}Query: {query}";
        }

    }
}

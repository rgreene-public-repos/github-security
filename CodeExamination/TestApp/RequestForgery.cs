namespace TestApp
{
    internal class RequestForgery
    {
        public async Task<string> GetRequest(string url)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}

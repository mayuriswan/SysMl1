namespace Project__1.Repository
{
    public class OpenDoorRepository : IOpenDoorRepository
    {
        private readonly HttpClient _httpClient;

        public OpenDoorRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> OpenDoorApiCallAsync()
        {
            // Example: Call the door API with authorization header
            var authorizationHeader = "Authorization: Basic YWRtaW46UGFzc3dvcmQwMQ==";
            _httpClient.DefaultRequestHeaders.Add("Authorization", authorizationHeader);

            // Adjust the URL and parameters based on your actual door API
            var apiUrl = "http://10.0.155.249/relay/0?turn=on";

            var response = await _httpClient.PostAsync(apiUrl, null);

            return response.IsSuccessStatusCode;
        }
    }
}

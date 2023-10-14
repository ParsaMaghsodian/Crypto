// See https://aka.ms/new-console-template for more information

using Crypto;
using Newtonsoft.Json;


await RunInBackground(TimeSpan.FromSeconds(1), () => InitAsync());

async Task RunInBackground(TimeSpan timeSpan, Action action)
    {
        var periodicTimer = new PeriodicTimer(timeSpan);
        while (await periodicTimer.WaitForNextTickAsync())
        {
            action();
        }
    }



    async Task InitAsync()
    {
    HttpClient httpclient = new HttpClient();
    string stringAPI = "https://api.wallex.ir/v1/currencies/stats";
    HttpResponseMessage response = await httpclient.GetAsync(stringAPI);
    if (response.IsSuccessStatusCode)
    {
        string apiresponse = await response.Content.ReadAsStringAsync();
        ApiResponseWrapper? apirapper = JsonConvert.DeserializeObject<ApiResponseWrapper>(apiresponse);
        List<DataItem>? dataitems = apirapper.result;
        foreach (var item in dataitems)
        {
            Console.WriteLine($"Key : {item.key}");
            Console.WriteLine($"Name : {item.name}");
            Console.WriteLine($"Price  : {item.price}");
            Console.WriteLine($"Rank :{item.rank}");
            Console.WriteLine($"updated_at :{item.updated_at}");
            Console.WriteLine($"created_at : {item.created_at}");
            double ? price = ((item.price) * 0.005);
            await Console.Out.WriteLineAsync($"Prediction is : {price}");


        }
    }
}














